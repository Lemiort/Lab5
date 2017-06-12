using Board_Developing_System_Prototype;
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

        int placerMethod;
        int tracerMethod;

        public Form1()
        {
            InitializeComponent();
            Computer comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown1.Value) * 1e6 };
            label4.Text = "Cost = " + comp1.Price + "$";
            comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown2.Value) * 1e6 };
            label5.Text = "Cost = " + comp1.Price + "$";
            comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown3.Value) * 1e6 };
            label7.Text = "Cost = " + comp1.Price + "$";
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
            IProcedure tracing = new Tracing();
            IProcedure displaying = new Displaying();
            IProcedure displaying2 = new Displaying();
            IProcedure evaluation = new Evaluation();
            IProcedure evaluation2 = new Evaluation();
            IProcedure storing = new Storing();
            Designer designer = new Designer();
            Designer designer2 = new Designer();
            Computer comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown1.Value)*1e6 };
            Computer comp2 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown3.Value) * 1e6 };
            Computer serv = new Computer { CpuFrequency = decimal.ToDouble(numericUpDown2.Value)*1e6 };
            Logger logger = new Logger();

            (input as IObservable).AddObserver(logger);
            (placing as IObservable).AddObserver(logger);
            (displaying as IObservable).AddObserver(logger);
            (displaying2 as IObservable).AddObserver(logger);
            (evaluation as IObservable).AddObserver(logger);
            (evaluation2 as IObservable).AddObserver(logger);
            (storing as IObservable).AddObserver(logger);
            (tracing as IObservable).AddObserver(logger);

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

            tracing.AddResource(comp2);
            tracing.AddResource(designer2);
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    tracing.AddResource(new TrunkTracingAlgorythm());
                    break;
                case 1:
                    tracing.AddResource(new WaveTracingAlgorythm());
                    break;
                case 2:
                    tracing.AddResource(new BeamTracingAlgorythm());
                    break;
                    break;
                default:
                    tracing.AddResource(new WaveTracingAlgorythm());
                    break;
            }


            displaying.AddResource(comp1);
            displaying.AddResource(designer);

            evaluation.AddResource(designer);
            evaluation.AddResource(comp1);

            //common input
            tempTask = input.PerformProcedure(tempTask);
            //placing step
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

            //do a job before tracing
            displaying2.AddResource(comp2);
            displaying2.AddResource(designer2);

            evaluation2.AddResource(designer2);
            evaluation2.AddResource(comp2);

            //tracing step
            do
            {
                tempTask = tracing.PerformProcedure(tempTask);
                tempTask = displaying2.PerformProcedure(tempTask);
                tempTask = evaluation2.PerformProcedure(tempTask);
                if (tempTask is Board)
                    if ((tempTask as Board).quality != Board.Quality.Bad)
                    {
                        break;
                    }
            } while (true);
            //change employee
            storing.RemoveResource(designer);
            storing.AddResource(designer2);
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
                "\nworkstation1 {1} GHz," +
                "\nsever {2} GHz," +
                "\nalgorithm {3}"+
                "\nworkstation2 {4} GHz," +
                "\nalgorithm {5}",
                cost, result.workstation.CpuFrequency/1e6, result.server.CpuFrequency/1e6, result.algorithm.ToString(),
                result.workstation2.CpuFrequency/1e6, result.algorithm2.ToString());
        }

        private TimeSpan AverageValueExperiment(Board board, PlacingAndTracingStepResources resources, int count)
        {
            TimeSpan result = new TimeSpan();
            for (int i = 0; i < count; i++)
            {
                IProcedure input = new Input();
                IProcedure placing = new Placing();
                IProcedure tracing = new Tracing();
                IProcedure displaying = new Displaying();
                IProcedure displaying2 = new Displaying();
                IProcedure evaluation = new Evaluation();
                IProcedure evaluation2 = new Evaluation();
                IProcedure storing = new Storing();
                Designer designer = resources.designer;
                Designer designer2 = resources.designer2;
                Computer comp1 = resources.workstation;
                Computer comp2 = resources.workstation2;
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

                //commont
                tempTask = input.PerformProcedure(tempTask);
                //placing
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

                //tracing
                //do a job before tracing
                tracing.AddResource(comp2);
                tracing.AddResource(resources.algorithm2);

                displaying2.AddResource(comp2);
                displaying2.AddResource(designer2);

                evaluation2.AddResource(designer2);
                evaluation2.AddResource(comp2);

                //tracing step
                do
                {
                    tempTask = tracing.PerformProcedure(tempTask);
                    tempTask = displaying2.PerformProcedure(tempTask);
                    tempTask = evaluation2.PerformProcedure(tempTask);
                    if (tempTask is Board)
                        if ((tempTask as Board).quality != Board.Quality.Bad)
                        {
                            break;
                        }
                } while (true);
                //change employee
                storing.RemoveResource(designer);
                storing.AddResource(designer2);
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

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Computer comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown3.Value) * 1e6 };
            label7.Text = "Cost = " + comp1.Price + "$";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AssessmentForm assForm = new AssessmentForm();
            assForm.ShowDialog();

            DirectEstimationMethod placerDEM = new DirectEstimationMethod(assForm.AssessmentPlacer, 2, assForm.ExpertCount(), 4);
            DirectEstimationMethod tracerDEM = new DirectEstimationMethod(assForm.AssessmentTracer, 3, assForm.ExpertCount(), 4);

            string str = "Эксперты выбрали:" + Environment.NewLine;
            this.placerMethod = placerDEM.CalculateBestMethod();
            this.tracerMethod = tracerDEM.CalculateBestMethod();
            comboBox1.SelectedIndex = placerMethod;
            comboBox2.SelectedIndex = tracerMethod;
        }
    }
}
