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
        }

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
            Computer comp1 = new Computer() { CpuFrequency = decimal.ToDouble(numericUpDown1.Value) };
            Computer serv = new Computer { CpuFrequency = decimal.ToDouble(numericUpDown2.Value) };
            Logger logger = new Logger();

            (input as IObservable).AddObserver(logger);
            (placing as IObservable).AddObserver(logger);
            (displaying as IObservable).AddObserver(logger);
            (evaluation as IObservable).AddObserver(logger);
            (storing as IObservable).AddObserver(logger);

            Board board = new Board(100, 100, 10);
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
                    placing.AddResource(new TrunkPlacingAlgorithm());
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
    }
}
