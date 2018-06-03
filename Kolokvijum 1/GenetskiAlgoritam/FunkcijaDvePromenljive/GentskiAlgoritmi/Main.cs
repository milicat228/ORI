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
            double rX = ga.algoritam();
            //TODO 17:Promeniti ispis
            double rX2 = ga.resenjeX2;
            double rY = GenetskiAlgoritam.funkcija(rX,rX2);
            
            if (GenetskiAlgoritam.traziMAX == true)
                label4.Text = "Resenje: f(" + rX.ToString("#0.000") + "," + rX2.ToString("#0.000") + ")=" + rY.ToString("#0.000");
            else
                label4.Text = "Resenje: f(" + rX.ToString("#0.000") + "," + rX2.ToString("#0.000") + ")=" + (-rY).ToString("#0.000");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //ovo je min dugme
            GenetskiAlgoritam ga = new GenetskiAlgoritam();
            GenetskiAlgoritam.traziMAX = false;
            double rX = ga.algoritam();
            //TODO 17:Promeniti ispis
            double rX2 = ga.resenjeX2;
            double rY = GenetskiAlgoritam.funkcija(rX, rX2);

            if (GenetskiAlgoritam.traziMAX == true)
                label4.Text = "Resenje: f(" + rX.ToString("#0.000") + "," + rX2.ToString("#0.000") + ")=" + rY.ToString("#0.000");
            else
                label4.Text = "Resenje: f(" + rX.ToString("#0.000") + "," + rX2.ToString("#0.000") + ")=" + (-rY).ToString("#0.000");
        }
    }
}
