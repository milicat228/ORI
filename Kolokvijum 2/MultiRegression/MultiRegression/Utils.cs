using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRegression
{
    public class Utils
    {
        public double[][] X;

        public void loadData(String name)
        {
            string fileName = string.Format(@"./data/{0}", name);
            string[] lines = File.ReadAllLines(fileName);
            lines = lines.Skip(1).ToArray();

            //incijalizuj           
            X = new double[lines.Length][];
            for(int i = 0; i < lines.Length; ++i)
            {
                X[i] = new double[4];
            }
           

            for(int i = 0; i < lines.Length; ++i)
            {
                String line = lines[i];
                String[] tokens = line.Split(',');

                //preuzmi ocekivanu vrednost
                X[i][3] = int.Parse(tokens[3]);

                X[i][0] = int.Parse(tokens[0]);
                X[i][1] = int.Parse(tokens[1]);
                X[i][2] = int.Parse(tokens[2]);

            }

            normalizeData(X);

        }

        private void normalizeData(double[][] input)
        {
            //trazenje min i max za svaku kolonu
            //mins sadrzi minimum svake kolone, maxs maksimum
            List<double> mins = new List<double>();
            List<double> maxs = new List<double>();
            for (int i = 0; i < input[0].Length; ++i)
            {
                mins.Add(input[0][i]);
                maxs.Add(input[0][i]);
            }

            for (int i = 1; i < input.Length; ++i)
            {
                for (int j = 0; j < input[i].Length; ++j)
                {
                    if (input[i][j] < mins[j])
                    {
                        mins[j] = input[i][j];
                    }

                    if (input[i][j] > maxs[j])
                    {
                        maxs[j] = input[i][j];
                    }
                }
            }

            //normalizacija vrednosti
            for (int i = 0; i < input.Length; ++i)
            {
                for (int j = 0; j < input[i].Length; ++j)
                {
                    input[i][j] = (input[i][j] - mins[j]) / (maxs[j] - mins[j]);
                }
            }
        }


    }
}
