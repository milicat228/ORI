using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GentskiAlgoritmi
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnAlgoritam_Click(object sender, EventArgs e)
        {
            GenetskiAlgoritam ga = new GenetskiAlgoritam();
            GenetskiAlgoritam.traziMAX = true;
            double[] rX = ga.algoritam();
            double rY = GenetskiAlgoritam.funkcija(rX);         
            String text = "Resenje: f(";
            foreach(double item in rX)
            {
                text += item.ToString("#0.000") + ", ";
            }
            text += ")=" + rY.ToString("#0.000");

            label4.Text = text;
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GenetskiAlgoritam ga = new GenetskiAlgoritam();
            GenetskiAlgoritam.traziMAX = false;
            double[] rX = ga.algoritam();
            double rY = GenetskiAlgoritam.funkcija(rX);
            String text = "Resenje: f(";
            foreach (double item in rX)
            {
                text += item.ToString("#0.000") + ", ";
            }
            text += ")=" + (-rY).ToString("#0.000");
           
            label4.Text = text;
        }
    }
}
