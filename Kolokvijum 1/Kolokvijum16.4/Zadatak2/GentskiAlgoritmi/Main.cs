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

        private void button1_Click_1(object sender, EventArgs e)
        {
            GenetskiAlgoritam ga = new GenetskiAlgoritam();           
            double rX = ga.algoritam();
            double rY = GenetskiAlgoritam.funkcija(rX);                 
            label4.Text = "Resenje: f(" + rX.ToString("#0.000") + ")=" + rY.ToString("#0.000");
            
        }
    }
}
