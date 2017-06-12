using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IM_2
{
    public partial class Form1 : Form
    {

        System.IO.StreamWriter file;

        public Form1()
        {
            InitializeComponent();
            Computer comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown1.Value) * 1e6 };
            label4.Text = "Cost = " + comp1.Price + "$";
            comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown2.Value) * 1e6 };
            label5.Text = "Cost = " + comp1.Price + "$";
        }


        /// <summary>
        /// одиночное испытание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            file = new System.IO.StreamWriter("output.txt",true);
            file.WriteLine("\n\n\n");

            IProcedure input = new Input();
            IProcedure placing = new Placing();
            IProcedure displaying = new Displaying();
            IProcedure evaluation = new Evaluation();
            IProcedure storing = new Storing();
            Designer designer = new Designer();
            Computer comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown1.Value)*1e6 };
            Computer serv = new Computer { CpuFrequency = decimal.ToDouble(numericUpDown2.Value)*1e6 };
            Logger logger = new Logger();

            (input as IObservable).AddObserver(logger);
            (placing as IObservable).AddObserver(logger);
            (displaying as IObservable).AddObserver(logger);
            (evaluation as IObservable).AddObserver(logger);
            (storing as IObservable).AddObserver(logger);

            Board board = new Board(100, 100, 100);
            AbsrtactTask tempTask = board;

            input.AddResource(designer);
            input.AddResource(comp1);

            placing.AddResource(comp1);
            placing.AddResource(designer);
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    placing.AddResource(new TightPlacingAlgorithm());
                    break;
                case 1:
                    placing.AddResource(new MatrixPlacingAlgorithm());
                    break;
                default:
                    placing.AddResource(new TightPlacingAlgorithm());
                    break;
            }
           

            displaying.AddResource(comp1);
            displaying.AddResource(designer);

            evaluation.AddResource(designer);
            evaluation.AddResource(comp1);

            tempTask = input.PerformProcedure(tempTask);
            do
            {
                tempTask = placing.PerformProcedure(tempTask);
                tempTask = displaying.PerformProcedure(tempTask);
                tempTask = evaluation.PerformProcedure(tempTask);
                if (tempTask is Board)
                    if ((tempTask as Board).quality != Board.Quality.Bad)
                    {
                        break;
                    }
            } while (true);
            storing.AddResource(serv);
            storing.AddResource(designer);
            tempTask = storing.PerformProcedure(tempTask);
            richTextBox1.Text += logger.Log;

            file.Write(logger.Log);
            file.Close();
        }

        /// <summary>
        /// оптимизация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "\n\n";
            SimulatedAnnealing optimizer = new SimulatedAnnealing();
            optimizer.StartTemperature = 9e3;//Its over 9000!
            optimizer.EndTemperature = 0;
            optimizer.DeltaTemperature = 10;
            double cost;
            var result = optimizer.Optimize(new Board(100, 100, 100), AverageValueExperiment, out cost);
            richTextBox1.Text +=
                String.Format("\nOptimal config in {0}$:" +
                "\nworkstation {1} GHz," +
                "\nsever {2} GHz," +
                "\nalgorithm {3}",
                cost, result.workstation.CpuFrequency/1e6, result.server.CpuFrequency/1e6, result.algorithm.ToString());
        }

        private TimeSpan AverageValueExperiment(Board board, PlacingStepResources resources, int count)
        {
            TimeSpan result = new TimeSpan();
            for (int i = 0; i < count; i++)
            {
                IProcedure input = new Input();
                IProcedure placing = new Placing();
                IProcedure displaying = new Displaying();
                IProcedure evaluation = new Evaluation();
                IProcedure storing = new Storing();
                Designer designer = resources.designer;
                Computer comp1 = resources.workstation;
                Computer serv = resources.server;
                AbsrtactTask tempTask = board;

                input.AddResource(designer);
                input.AddResource(comp1);

                placing.AddResource(comp1);
                placing.AddResource(designer);
                placing.AddResource(resources.algorithm);



                displaying.AddResource(comp1);
                displaying.AddResource(designer);

                evaluation.AddResource(designer);
                evaluation.AddResource(comp1);

                tempTask = input.PerformProcedure(tempTask);
                do
                {
                    tempTask = placing.PerformProcedure(tempTask);
                    tempTask = displaying.PerformProcedure(tempTask);
                    tempTask = evaluation.PerformProcedure(tempTask);
                    if (tempTask is Board)
                        if ((tempTask as Board).quality != Board.Quality.Bad)
                        {
                            break;
                        }
                } while (true);
                storing.AddResource(serv);
                storing.AddResource(designer);
                tempTask = storing.PerformProcedure(tempTask);
                result += (tempTask as Board).DevelopTime;
            }
            return TimeSpan.FromHours(result.TotalHours/count);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Computer comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown1.Value) * 1e6 };
            label4.Text = "Cost = " + comp1.Price + "$";
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Computer comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown2.Value) * 1e6 };
            label5.Text = "Cost = " + comp1.Price + "$";
        }
    }
}
