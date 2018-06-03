using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ComputationalGraph
{
    
    public class Utils
    {
        private String[] lines;       
        public List<List<double>> test = new List<List<double>>();        
        public List<List<double>> testY = new List<List<double>>();
        public List<List<double>> train = new List<List<double>>();        
        public List<List<double>> trainY = new List<List<double>>();

        public void readFile()
        {
            //ucitaj train set
            lines = File.ReadAllLines(@"./../../train.csv");            
            lines = lines.Skip(1).ToArray();
            
            foreach(String line in lines)
            {
                parseLine(line, train, trainY);
            }

            //ucitaj teset set
            lines = File.ReadAllLines(@"./../../train.csv");
            lines = lines.Skip(1).ToArray();

            foreach (String line in lines)
            {
                parseLine(line, test, testY);
            }

            normalize(test);
            normalize(train);

        }

        
        private void parseLine(String line, List<List<double>> input, List<List<Double>> output)
        {
            String[] tokens = line.Split(',');

            //parsiraj ulaz
            double col_1 = Double.Parse(tokens[1]);
            double col_2 = Double.Parse(tokens[2]);
            double col_3 = Double.Parse(tokens[3]);
            double col_4 = Double.Parse(tokens[4]);
            input.Add(new List<double>() { col_1, col_2, col_3, col_4});           

            //parsiraj izlaz
            String type = tokens[5];           
            if (type.Equals("type_1"))
            {
                output.Add(new List<double>() { 0 });              
            }
            else if (type.Equals("type_2"))
            {
                output.Add(new List<double>() { 0.5 });               
            }
            else
            {
                output.Add(new List<double>() { 1 });                
            }
        }

        //normalizacija svih podataka -> svi se svode na [0,1]
        //formula: zi = (xi - min(x)) / (max(x) - min(x))
        //x - vektor svih vrednosti nekog ulaza (npr col_1 je x)
        //xi - vrednost i-tog elementa u vektor x
        //zi - normalizovana vrednost x
        public void normalize(List<List<double>> input)
        {
            //trazenje min i max za svaku kolonu
            //mins sadrzi minimum svake kolone, maxs maksimum
            List<double> mins = new List<double>();
            List<double> maxs = new List<double>();
            for(int i = 0; i < input[0].Count; ++i)
            {
                mins.Add(input[0][i]);
                maxs.Add(input[0][i]);
            }

            for(int i = 1; i < input.Count; ++i)
            {
                for(int j = 0; j < input[i].Count; ++j)
                {
                    if( input[i][j] < mins[j])
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
            for (int i = 0; i < input.Count; ++i)
            {
                for (int j = 0; j < input[i].Count; ++j)
                {
                    input[i][j] = (input[i][j] - mins[j]) / (maxs[j] - mins[j]);
                }
            }
        }

    }
}
