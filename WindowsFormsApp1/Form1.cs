using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        double[] aboba = new double[5];
        double hit = 9.488f;

        public Form1()
        {
            InitializeComponent();
            StartB.Enabled = false;
        }

        private void Num5B_Click(object sender, EventArgs e)
        {
            aboba[0] = (double)Num1.Value;
            aboba[1] = (double)Num2.Value;
            aboba[2] = (double)Num3.Value;
            aboba[3] = (double)Num4.Value;
            aboba[4] = 1 - (aboba[0] + aboba[1] + aboba[2] + aboba[3]);
            if (aboba[4] <= 0)
            {
                RelA.Text = "The sum of the first four probabilities is greater than 1";
                return;
            }
            else
            {
                Num5B.Text = aboba[4]+"";
                StartB.Enabled = true;
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            Random rand = new Random();

            int[] stats = new int[5];
            var n = (int)Trials.Value;

            for (int i = 0; i<n; i++)
            {
                var q = (double)rand.NextDouble();
                for (int j=0; j<5; j++)
                {
                    q -= aboba[j];
                    if (q <= 0)
                    {
                        stats[j]++;
                        break;
                    }
                }
            }
            double[] brat_aboba = new double[5];
            double Cs = 0;
            
            for (int i = 0; i<5; i++)
            {
                brat_aboba[i] = (double)stats[i] / n;
                Cs += (double)Math.Pow(stats[i], 2) / (aboba[i] * n);
                chart1.Series[0].Points.AddXY(i+1,(double)stats[i]/n);
            }
            Cs -= n;

            double chiki1 = 0, chiki2 = 0, briki1 = 0, briki2 = 0;
            for (int i=0;i<5;i++)
            {
                chiki1 += (i + 1) * aboba[i];
                chiki2 += (i + 1) * brat_aboba[i];
                briki1 += (double)Math.Pow(i + 1, 2) * aboba[i];
                briki2 += (double)Math.Pow(i + 1, 2) * brat_aboba[i];
            }

            briki1 -= (double)Math.Pow(chiki1, 2);
            briki2 -= (double)Math.Pow(chiki2, 2);

            RelA.Text = $"{chiki2} <Error = {Math.Round(Math.Abs(chiki2 - chiki1) / Math.Abs(chiki2) * 100)}%>";
            RelV.Text = $"{briki2} <Error = {Math.Round(Math.Abs(briki2 - briki1) / Math.Abs(briki2) * 100, 2)}%>";

            RelC.Text = Cs < hit ? $"{Cs} < {hit}  is {false}" : $"{Cs} > {hit}  is {true}";
        }
    }
}