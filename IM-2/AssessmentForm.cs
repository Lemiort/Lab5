using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Board_Developing_System_Prototype
{  

    public partial class AssessmentForm : Form
    {

        //tracerPanel1

        private List<NumericUpDown> placer1Parameter1Numerics;
        private List<NumericUpDown> placer1Parameter2Numerics;
        private List<NumericUpDown> placer1Parameter3Numerics;
        private List<NumericUpDown> placer1Parameter4Numerics;

        private List<NumericUpDown> placer2Parameter1Numerics;
        private List<NumericUpDown> placer2Parameter2Numerics;
        private List<NumericUpDown> placer2Parameter3Numerics;
        private List<NumericUpDown> placer2Parameter4Numerics;

        private List<NumericUpDown> tracer1Parameter1Numerics;
        private List<NumericUpDown> tracer1Parameter2Numerics;
        private List<NumericUpDown> tracer1Parameter3Numerics;
        private List<NumericUpDown> tracer1Parameter4Numerics;

        private List<NumericUpDown> tracer2Parameter1Numerics;
        private List<NumericUpDown> tracer2Parameter2Numerics;
        private List<NumericUpDown> tracer2Parameter3Numerics;
        private List<NumericUpDown> tracer2Parameter4Numerics;

        private List<NumericUpDown> tracer3Parameter1Numerics;
        private List<NumericUpDown> tracer3Parameter2Numerics;
        private List<NumericUpDown> tracer3Parameter3Numerics;
        private List<NumericUpDown> tracer3Parameter4Numerics;

        private double[,,] assessmentPlacer;

        private double[,,] assessmentTracer;

        public double[,,] AssessmentPlacer
        {
            get
            {
                return assessmentPlacer;
            }
        }

        public double[,,] AssessmentTracer
        {
            get
            {
                return assessmentTracer;
            }
        }

        public AssessmentForm()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void InitPlacePanel1()
        {
            int n = (int)expertCountNumeric.Value;

            placer1Parameter1Numerics = new List<NumericUpDown>();
            placer1Parameter2Numerics = new List<NumericUpDown>();
            placer1Parameter3Numerics = new List<NumericUpDown>();
            placer1Parameter4Numerics = new List<NumericUpDown>();

            placePanel1.Controls.Clear();

            for (int i = 0; i < n; i ++)
            {
                Label lab1 = new Label();
                lab1.Text = "Стоимость:";
                lab1.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num1 = new NumericUpDown();
                num1.Minimum = new decimal(0);
                num1.Maximum = new decimal(10);
                num1.Size = new Size(36, 20);
                
                placer1Parameter1Numerics.Add(num1);

                Label lab2 = new Label();
                lab2.Text = "Произв-ть:";
                lab2.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num2 = new NumericUpDown();
                num2.Minimum = new decimal(0);
                num2.Maximum = new decimal(10);
                num2.Size = new Size(36, 20);

                placer1Parameter2Numerics.Add(num2);

                Label lab3 = new Label();
                lab3.Text = "Надёжность:";
                lab3.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num3 = new NumericUpDown();
                num3.Minimum = new decimal(0);
                num3.Maximum = new decimal(10);
                num3.Size = new Size(36, 20);

                placer1Parameter3Numerics.Add(num3);

                Label lab4 = new Label();
                lab4.Text = "Комфортность:";
                lab4.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num4 = new NumericUpDown();
                num4.Minimum = new decimal(0);
                num4.Maximum = new decimal(10);
                num4.Size = new Size(36, 20);

                placer1Parameter4Numerics.Add(num4);

                placePanel1.Controls.Add(lab1);
                placePanel1.Controls.Add(num1);

                placePanel1.Controls.Add(lab2);
                placePanel1.Controls.Add(num2);

                placePanel1.Controls.Add(lab3);
                placePanel1.Controls.Add(num3);

                placePanel1.Controls.Add(lab4);
                placePanel1.Controls.Add(num4);
            }
        }

        private void InitPlacePanel2()
        {
            int n = (int)expertCountNumeric.Value;

            placer2Parameter1Numerics = new List<NumericUpDown>();
            placer2Parameter2Numerics = new List<NumericUpDown>();
            placer2Parameter3Numerics = new List<NumericUpDown>();
            placer2Parameter4Numerics = new List<NumericUpDown>();

            placePanel2.Controls.Clear();

            for (int i = 0; i < n; i++)
            {
                Label lab1 = new Label();
                lab1.Text = "Стоимость:";
                lab1.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num1 = new NumericUpDown();
                num1.Minimum = new decimal(0);
                num1.Maximum = new decimal(10);
                num1.Size = new Size(36, 20);

                placer2Parameter1Numerics.Add(num1);

                Label lab2 = new Label();
                lab2.Text = "Произв-ть:";
                lab2.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num2 = new NumericUpDown();
                num2.Minimum = new decimal(0);
                num2.Maximum = new decimal(10);
                num2.Size = new Size(36, 20);

                placer2Parameter2Numerics.Add(num2);

                Label lab3 = new Label();
                lab3.Text = "Надёжность:";
                lab3.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num3 = new NumericUpDown();
                num3.Minimum = new decimal(0);
                num3.Maximum = new decimal(10);
                num3.Size = new Size(36, 20);

                placer2Parameter3Numerics.Add(num3);

                Label lab4 = new Label();
                lab4.Text = "Комфортность:";
                lab4.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num4 = new NumericUpDown();
                num4.Minimum = new decimal(0);
                num4.Maximum = new decimal(10);
                num4.Size = new Size(36, 20);

                placer2Parameter4Numerics.Add(num4);

                placePanel2.Controls.Add(lab1);
                placePanel2.Controls.Add(num1);

                placePanel2.Controls.Add(lab2);
                placePanel2.Controls.Add(num2);

                placePanel2.Controls.Add(lab3);
                placePanel2.Controls.Add(num3);

                placePanel2.Controls.Add(lab4);
                placePanel2.Controls.Add(num4);
            }
        }

        private void InitTracerPanel1()
        {
            int n = (int)expertCountNumeric.Value;

            tracer1Parameter1Numerics = new List<NumericUpDown>();
            tracer1Parameter2Numerics = new List<NumericUpDown>();
            tracer1Parameter3Numerics = new List<NumericUpDown>();
            tracer1Parameter4Numerics = new List<NumericUpDown>();

            tracerPanel1.Controls.Clear();

            for (int i = 0; i < n; i++)
            {
                Label lab1 = new Label();
                lab1.Text = "Стоимость:";
                lab1.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num1 = new NumericUpDown();
                num1.Minimum = new decimal(0);
                num1.Maximum = new decimal(10);
                num1.Size = new Size(36, 20);

                tracer1Parameter1Numerics.Add(num1);

                Label lab2 = new Label();
                lab2.Text = "Произв-ть:";
                lab2.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num2 = new NumericUpDown();
                num2.Minimum = new decimal(0);
                num2.Maximum = new decimal(10);
                num2.Size = new Size(36, 20);

                tracer1Parameter2Numerics.Add(num2);

                Label lab3 = new Label();
                lab3.Text = "Надёжность:";
                lab3.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num3 = new NumericUpDown();
                num3.Minimum = new decimal(0);
                num3.Maximum = new decimal(10);
                num3.Size = new Size(36, 20);

                tracer1Parameter3Numerics.Add(num3);

                Label lab4 = new Label();
                lab4.Text = "Комфортность:";
                lab4.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num4 = new NumericUpDown();
                num4.Minimum = new decimal(0);
                num4.Maximum = new decimal(10);
                num4.Size = new Size(36, 20);

                tracer1Parameter4Numerics.Add(num4);

                tracerPanel1.Controls.Add(lab1);
                tracerPanel1.Controls.Add(num1);

                tracerPanel1.Controls.Add(lab2);
                tracerPanel1.Controls.Add(num2);

                tracerPanel1.Controls.Add(lab3);
                tracerPanel1.Controls.Add(num3);

                tracerPanel1.Controls.Add(lab4);
                tracerPanel1.Controls.Add(num4);
            }
        }

        private void InitTracerPanel2()
        {
            int n = (int)expertCountNumeric.Value;

            tracer2Parameter1Numerics = new List<NumericUpDown>();
            tracer2Parameter2Numerics = new List<NumericUpDown>();
            tracer2Parameter3Numerics = new List<NumericUpDown>();
            tracer2Parameter4Numerics = new List<NumericUpDown>();

            tracerPanel2.Controls.Clear();

            for (int i = 0; i < n; i++)
            {
                Label lab1 = new Label();
                lab1.Text = "Стоимость:";
                lab1.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num1 = new NumericUpDown();
                num1.Minimum = new decimal(0);
                num1.Maximum = new decimal(10);
                num1.Size = new Size(36, 20);

                tracer2Parameter1Numerics.Add(num1);

                Label lab2 = new Label();
                lab2.Text = "Произв-ть:";
                lab2.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num2 = new NumericUpDown();
                num2.Minimum = new decimal(0);
                num2.Maximum = new decimal(10);
                num2.Size = new Size(36, 20);

                tracer2Parameter2Numerics.Add(num2);

                Label lab3 = new Label();
                lab3.Text = "Надёжность:";
                lab3.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num3 = new NumericUpDown();
                num3.Minimum = new decimal(0);
                num3.Maximum = new decimal(10);
                num3.Size = new Size(36, 20);

                tracer2Parameter3Numerics.Add(num3);

                Label lab4 = new Label();
                lab4.Text = "Комфортность:";
                lab4.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num4 = new NumericUpDown();
                num4.Minimum = new decimal(0);
                num4.Maximum = new decimal(10);
                num4.Size = new Size(36, 20);

                tracer2Parameter4Numerics.Add(num4);

                tracerPanel2.Controls.Add(lab1);
                tracerPanel2.Controls.Add(num1);

                tracerPanel2.Controls.Add(lab2);
                tracerPanel2.Controls.Add(num2);

                tracerPanel2.Controls.Add(lab3);
                tracerPanel2.Controls.Add(num3);

                tracerPanel2.Controls.Add(lab4);
                tracerPanel2.Controls.Add(num4);
            }
        }

        private void InitTracerPanel3()
        {
            int n = (int)expertCountNumeric.Value;

            tracer3Parameter1Numerics = new List<NumericUpDown>();
            tracer3Parameter2Numerics = new List<NumericUpDown>();
            tracer3Parameter3Numerics = new List<NumericUpDown>();
            tracer3Parameter4Numerics = new List<NumericUpDown>();

            tracerPanel3.Controls.Clear();

            for (int i = 0; i < n; i++)
            {
                Label lab1 = new Label();
                lab1.Text = "Стоимость:";
                lab1.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num1 = new NumericUpDown();
                num1.Minimum = new decimal(0);
                num1.Maximum = new decimal(10);
                num1.Size = new Size(36, 20);

                tracer3Parameter1Numerics.Add(num1);

                Label lab2 = new Label();
                lab2.Text = "Произв-ть:";
                lab2.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num2 = new NumericUpDown();
                num2.Minimum = new decimal(0);
                num2.Maximum = new decimal(10);
                num2.Size = new Size(36, 20);

                tracer3Parameter2Numerics.Add(num2);

                Label lab3 = new Label();
                lab3.Text = "Надёжность:";
                lab3.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num3 = new NumericUpDown();
                num3.Minimum = new decimal(0);
                num3.Maximum = new decimal(10);
                num3.Size = new Size(36, 20);

                tracer3Parameter3Numerics.Add(num3);

                Label lab4 = new Label();
                lab4.Text = "Комфортность:";
                lab4.Padding = new Padding(0, 5, 0, 0);

                NumericUpDown num4 = new NumericUpDown();
                num4.Minimum = new decimal(0);
                num4.Maximum = new decimal(10);
                num4.Size = new Size(36, 20);

                tracer3Parameter4Numerics.Add(num4);

                tracerPanel3.Controls.Add(lab1);
                tracerPanel3.Controls.Add(num1);

                tracerPanel3.Controls.Add(lab2);
                tracerPanel3.Controls.Add(num2);

                tracerPanel3.Controls.Add(lab3);
                tracerPanel3.Controls.Add(num3);

                tracerPanel3.Controls.Add(lab4);
                tracerPanel3.Controls.Add(num4);
            }
        }

        private void AssessmentForm_Load(object sender, EventArgs e)
        {
            InitPlacePanel1();
            InitPlacePanel2();
            InitTracerPanel1();
            InitTracerPanel2();
            InitTracerPanel3();
        }

        private void expertCountNumeric_ValueChanged(object sender, EventArgs e)
        {
            InitPlacePanel1();
            InitPlacePanel2();
            InitTracerPanel1();
            InitTracerPanel2();
            InitTracerPanel3();
        }

        public int ExpertCount()
        {
            return (int)this.expertCountNumeric.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

            foreach (NumericUpDown num in placer1Parameter1Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in placer1Parameter2Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in placer1Parameter3Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in placer1Parameter4Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in placer2Parameter1Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in placer2Parameter2Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in placer2Parameter3Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in placer2Parameter4Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer1Parameter1Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer1Parameter2Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer1Parameter3Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer1Parameter4Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer2Parameter1Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer2Parameter2Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer2Parameter3Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer2Parameter4Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer3Parameter1Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer3Parameter2Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer3Parameter3Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }

            foreach (NumericUpDown num in tracer3Parameter4Numerics)
            {
                num.Value = new decimal(rand.Next(0, 11));
            }
        }

        private void AssessmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            assessmentPlacer = new double[2, (int)this.expertCountNumeric.Value, 4];
            assessmentTracer = new double[3, (int)this.expertCountNumeric.Value, 4];
            for (int i = 0; i < (int)expertCountNumeric.Value; i++)
            {
                assessmentPlacer[0, i, 0] = (double)placer1Parameter1Numerics[i].Value;
                assessmentPlacer[0, i, 1] = (double)placer1Parameter2Numerics[i].Value;
                assessmentPlacer[0, i, 2] = (double)placer1Parameter3Numerics[i].Value;
                assessmentPlacer[0, i, 3] = (double)placer1Parameter4Numerics[i].Value;

                assessmentPlacer[1, i, 0] = (double)placer2Parameter1Numerics[i].Value;
                assessmentPlacer[1, i, 1] = (double)placer2Parameter2Numerics[i].Value;
                assessmentPlacer[1, i, 2] = (double)placer2Parameter3Numerics[i].Value;
                assessmentPlacer[1, i, 3] = (double)placer2Parameter4Numerics[i].Value;

                assessmentTracer[0, i, 0] = (double)tracer1Parameter1Numerics[i].Value;
                assessmentTracer[0, i, 1] = (double)tracer1Parameter2Numerics[i].Value;
                assessmentTracer[0, i, 2] = (double)tracer1Parameter3Numerics[i].Value;
                assessmentTracer[0, i, 3] = (double)tracer1Parameter4Numerics[i].Value;

                assessmentTracer[1, i, 0] = (double)tracer2Parameter1Numerics[i].Value;
                assessmentTracer[1, i, 1] = (double)tracer2Parameter2Numerics[i].Value;
                assessmentTracer[1, i, 2] = (double)tracer2Parameter3Numerics[i].Value;
                assessmentTracer[1, i, 3] = (double)tracer2Parameter4Numerics[i].Value;

                assessmentTracer[2, i, 0] = (double)tracer3Parameter1Numerics[i].Value;
                assessmentTracer[2, i, 1] = (double)tracer3Parameter2Numerics[i].Value;
                assessmentTracer[2, i, 2] = (double)tracer3Parameter3Numerics[i].Value;
                assessmentTracer[2, i, 3] = (double)tracer3Parameter4Numerics[i].Value;
            }
        }
    }
}
