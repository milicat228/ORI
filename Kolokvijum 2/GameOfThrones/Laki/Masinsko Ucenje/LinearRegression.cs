using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masinsko_Ucenje
{
    public class LinearRegression
    {
        public double k { get; set; }
        public double n { get; set; }

	    public void fit(double[] x, double[] y) {
            // TODO 2: implementirati fit funkciju koja odredjuje parametre k i n
            // y = kx + n

            double sumaX = 0;
            double sumaY = 0;
            double sumaXY = 0;
            double sumaX2 = 0;

            for(int i = 0; i < x.Length; ++i)
            {
                sumaX += x[i];
                sumaY += y[i];
                sumaXY += x[i] * y[i];
                sumaX2 += x[i] * x[i];
            }

            k = (x.Length * sumaXY - sumaX * sumaY) / (x.Length * sumaX2 - sumaX * sumaX);
            n = (sumaY - k * sumaX) / x.Length;

	    }

        public double predict(double x)
        {   
            // TODO 3: Implementirati funkciju predict koja na osnovu x vrednosti vraca
            // predvinjenu vrednost y
            return k*x + n;
        }
    }
}
