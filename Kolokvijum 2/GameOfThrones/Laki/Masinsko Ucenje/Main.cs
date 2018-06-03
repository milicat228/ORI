using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace Masinsko_Ucenje
{
    public partial class Main : Form
    {

        LinearRegression regression;      
        string[] lines;

        private int indexOfPopularity = 0;
        private int indexOfDeadTheyKnew = 0;


        public Main()
        {
            InitializeComponent();

            lines = File.ReadAllLines(@"./../../data/dataset.csv");

            //TODO 1.1: Koga mrzi da rucno broji koji atribut mu treba
            //napomena: dataset ima jako puno podataka i trebace vremena da se izvrsi sve :)
            String[] titles = lines[0].Split(',');
            for(int i = 0; i < titles.Length; ++i)
            {
                String title = titles[i];
                if (title.Equals("numDeadRelations"))
                {
                    indexOfDeadTheyKnew = i;
                }
                if (title.Equals("popularity"))
                {
                    indexOfPopularity = i;
                }
            }

            lines = lines.Skip(1).ToArray();
        }

        private void btnLinearRegression_Click(object sender, EventArgs e)
        {
            regression = new LinearRegression();
            List<double> x = new List<double>();
            List<double> y = new List<double>();

            // TODO 1.2: Ucitati i isparsirati skup podataka iz lines u x i y
            foreach(String line in lines)
            {
                String[] parts = line.Split(',');
                double deadPeopleTheyKnew = Double.Parse(parts[indexOfDeadTheyKnew]);
                double popular = Double.Parse(parts[indexOfPopularity]);
                x.Add(deadPeopleTheyKnew);
                y.Add(popular);
            }

            // TODO 4: Izvršiti linearnu regresiju na primeru predviđanja stope 
            // smrtnosti od raka kože na osnovu geografske širine američkih država.
            regression.fit(x.ToArray(), y.ToArray());

            //TODO 5: Ispisati za 5,10 i 15
            textBox1.Text = regression.predict(5).ToString();
            textBox2.Text = regression.predict(10).ToString();
            textBox3.Text = regression.predict(15).ToString();

            // draw regresiion line on a chart
            drawRegressionResults(x, y);          
        }

       
        #region GUI_Functions
        private void drawRegressionResults(List<double> X, List<double> Y)
        {
            RegressionChart.Visible = true;
            ClusteringChart.Visible = false;
            RegressionChart.Series.Clear();

            Series diagramLimitsSeries = new Series("DiagramLimits");
            diagramLimitsSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            diagramLimitsSeries.Points.AddXY(X.Min(), Y.Min());
            diagramLimitsSeries.Points.AddXY(X.Max(), Y.Max());
            diagramLimitsSeries.Points[0].IsEmpty = true;
            diagramLimitsSeries.Points[1].IsEmpty = true;
            diagramLimitsSeries.IsVisibleInLegend = false;
            RegressionChart.Series.Add(diagramLimitsSeries);
            RegressionChart.Update();


            // Create a point series.
            Series pointSeries = new Series("Tacke");
            pointSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            RegressionChart.Series.Add(pointSeries);

            for (int i = 0; i < X.Count; i++)
            {
                pointSeries.Points.AddXY(X[i], Y[i]);
                //Thread.Sleep(5);
                RegressionChart.Update();
            }

            // Create a line series.
            string lineLabel = "";
            if (regression.n > 0)
                lineLabel = "Regresiona prava: y=" + Math.Round(regression.k,2) + "*x + " + Math.Round(regression.n,2);
            else
                lineLabel = "Regresiona prava: y=" + Math.Round(regression.k,2) + "*x - " + Math.Round(Math.Abs(regression.n),2);
            Series lineSeries = new Series(lineLabel);
            lineSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            lineSeries.Points.AddXY(X.Max(), regression.n + X.Max() * regression.k);
            lineSeries.Points.AddXY(X.Min(), regression.n + X.Min() * regression.k);
            RegressionChart.Series.Add(lineSeries);
            RegressionChart.Update();
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion GUI_Functions
    }
}
