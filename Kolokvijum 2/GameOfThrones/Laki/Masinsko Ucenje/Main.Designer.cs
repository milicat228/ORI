namespace Masinsko_Ucenje
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.RegressionChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnLinearRegression = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ClusteringChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.RegressionChart)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClusteringChart)).BeginInit();
            this.SuspendLayout();
            // 
            // RegressionChart
            // 
            chartArea7.AxisX.IsStartedFromZero = false;
            chartArea7.AxisX2.IsStartedFromZero = false;
            chartArea7.AxisY.IsStartedFromZero = false;
            chartArea7.AxisY2.IsStartedFromZero = false;
            chartArea7.Name = "ChartArea1";
            this.RegressionChart.ChartAreas.Add(chartArea7);
            this.RegressionChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend7.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend7.Name = "Legend1";
            legend7.TextWrapThreshold = 150;
            this.RegressionChart.Legends.Add(legend7);
            this.RegressionChart.Location = new System.Drawing.Point(0, 0);
            this.RegressionChart.Margin = new System.Windows.Forms.Padding(4);
            this.RegressionChart.Name = "RegressionChart";
            this.RegressionChart.Size = new System.Drawing.Size(831, 752);
            this.RegressionChart.TabIndex = 0;
            this.RegressionChart.Text = "chart1";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(28, 27);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(178, 25);
            this.Label1.TabIndex = 13;
            this.Label1.Text = "Regresiona analiza";
            // 
            // btnLinearRegression
            // 
            this.btnLinearRegression.Location = new System.Drawing.Point(33, 66);
            this.btnLinearRegression.Margin = new System.Windows.Forms.Padding(4);
            this.btnLinearRegression.Name = "btnLinearRegression";
            this.btnLinearRegression.Size = new System.Drawing.Size(172, 37);
            this.btnLinearRegression.TabIndex = 20;
            this.btnLinearRegression.Text = "Linearna regresija";
            this.btnLinearRegression.UseVisualStyleBackColor = true;
            this.btnLinearRegression.Click += new System.EventHandler(this.btnLinearRegression_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnLinearRegression);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(831, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 752);
            this.panel1.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 17);
            this.label5.TabIndex = 24;
            this.label5.Text = "Povezan sa 10 ubijenih:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "Povezan sa 15 ubijenih:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Povezan sa 5 ubijenih:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "label2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ClusteringChart);
            this.panel2.Controls.Add(this.RegressionChart);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(831, 752);
            this.panel2.TabIndex = 22;
            // 
            // ClusteringChart
            // 
            chartArea8.AxisX.IsStartedFromZero = false;
            chartArea8.AxisX2.IsStartedFromZero = false;
            chartArea8.AxisY.IsStartedFromZero = false;
            chartArea8.AxisY2.IsStartedFromZero = false;
            chartArea8.Name = "ChartArea1";
            this.ClusteringChart.ChartAreas.Add(chartArea8);
            this.ClusteringChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend8.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend8.Name = "Legend1";
            legend8.TextWrapThreshold = 150;
            this.ClusteringChart.Legends.Add(legend8);
            this.ClusteringChart.Location = new System.Drawing.Point(0, 0);
            this.ClusteringChart.Margin = new System.Windows.Forms.Padding(4);
            this.ClusteringChart.Name = "ClusteringChart";
            this.ClusteringChart.Size = new System.Drawing.Size(831, 752);
            this.ClusteringChart.TabIndex = 1;
            this.ClusteringChart.Text = "chart1";
            // 
            // textBox1
            // 
            this.textBox1.AccessibleName = "5mrtvih";
            this.textBox1.Location = new System.Drawing.Point(187, 136);
            this.textBox1.Name = "5mrtvih";
            this.textBox1.Size = new System.Drawing.Size(66, 22);
            this.textBox1.TabIndex = 25;
            // 
            // textBox2
            // 
            this.textBox2.AccessibleName = "10mrtvih";
            this.textBox2.Location = new System.Drawing.Point(187, 191);
            this.textBox2.Name = "10mrtvih";
            this.textBox2.Size = new System.Drawing.Size(66, 22);
            this.textBox2.TabIndex = 26;
            // 
            // textBox3
            // 
            this.textBox3.AccessibleName = "15mrtvih";
            this.textBox3.Location = new System.Drawing.Point(187, 243);
            this.textBox3.Name = "15mrtvih";
            this.textBox3.Size = new System.Drawing.Size(66, 22);
            this.textBox3.TabIndex = 27;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 752);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.RegressionChart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ClusteringChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart RegressionChart;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnLinearRegression;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart ClusteringChart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

