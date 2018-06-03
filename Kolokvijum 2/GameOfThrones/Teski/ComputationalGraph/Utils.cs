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
        private String[] titles;
        private Dictionary<String, double> houses = new Dictionary<String, double>();

        public void readFile()
        {
            lines = File.ReadAllLines(@"./../../dataset.csv");
            titles = lines[0].Split(',');
            lines = lines.Skip(1).ToArray();
            //podeliti train i test u odnosu 20% test, 80% train
            int whereToSplit = lines.Length * 20/100;
            int index = 0;

            for(int i = 0; i < lines.Length; ++i)
            {
                String line = lines[i];
                String[] tokens = line.Split(',');

                //isAlive se predvidja
                index = findTitle("isAlive");
                double isAlive = Double.Parse(tokens[index]);

                //polja koja se koriste kao ulazi
                index = findTitle("male");
                double male = Double.Parse(tokens[index]);
                index = findTitle("popularity");
                double popularity = Double.Parse(tokens[index]);
                index = findTitle("isNoble");
                double isNoble = Double.Parse(tokens[index]);
                index = findTitle("numDeadRelations");
                double nomDeadRelations = Double.Parse(tokens[index]);
                double books = bookCount(tokens);
                index = findTitle("house");
                double house = whatsMyHouse(tokens[index]);

                if ( i > whereToSplit)
                {
                    //dodaj u train
                    trainY.Add(new List<double>() { isAlive });
                    train.Add(new List<double>() { male, popularity, isNoble, nomDeadRelations, books, house });
                }
                else
                {
                    //dodaj u test
                    testY.Add(new List<double>() { isAlive });
                    test.Add(new List<double>() { male, popularity, isNoble, nomDeadRelations, books, house });
                }

            }

            /* Ovde skoro da ne utice, a na SIIT zadatku ne moze bez toga
            normalize(test);
            normalize(train);*/
        }

        private double whatsMyHouse(String house)
        {
            if( houses.ContainsKey(house))
            {               
                return houses[house];
            }
            else
            {
                //kuca nije u recniku, dodaj je
                houses.Add(house, houses.Count);
                return houses[house];
            }


        }

        private double bookCount(String[] tokens)
        {
            double ret = 0;

            int index = findTitle("book1");
            ret += Double.Parse(tokens[index]);

            index = findTitle("book2");
            ret += Double.Parse(tokens[index]);

            index = findTitle("book3");
            ret += Double.Parse(tokens[index]);

            index = findTitle("book4");
            ret += Double.Parse(tokens[index]);

            index = findTitle("book5");
            ret += Double.Parse(tokens[index]);

            return ret;
        }

        private int findTitle(String title)
        {
            for (int i = 0; i < titles.Length; ++i)
            {
                if (title.Equals(titles[i]))
                {
                    return i;
                }
            }

            return -1;
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
            for (int i = 0; i < input[0].Count; ++i)
            {
                mins.Add(input[0][i]);
                maxs.Add(input[0][i]);
            }

            for (int i = 1; i < input.Count; ++i)
            {
                for (int j = 0; j < input[i].Count; ++j)
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
