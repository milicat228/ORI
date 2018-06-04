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
        private Dictionary<String, double> neighbourhoods = new Dictionary<string, double>();
        private Dictionary<String, double> buildingTypes = new Dictionary<string, double>();
        public List<int> MSSubClassValue = new List<int>() { 20, 30, 40, 45, 50, 60, 70, 75, 80, 85, 90, 120, 150, 160, 180, 190 };

        public void readFile()
        {
            lines = File.ReadAllLines(@"./../../train.csv");
            titles = lines[0].Split(',');
            lines = lines.Skip(1).ToArray();
            //podeliti train i test u odnosu 20% test, 80% train
            int whereToSplit = lines.Length * 20 / 100;
            int index = 0;

            for (int i = 0; i < lines.Length; ++i)
            {
                String line = lines[i];
                String[] tokens = line.Split(',');

                //polja koja se koriste kao ulazi
                index = findTitle("Neighborhood");
                double neighborhood = parseNeighborhood(tokens[index]);

                index = findTitle("LotArea");
                double lotArea = Double.Parse(tokens[index]);

                index = findTitle("BldgType");
                double bldgType = parseBuildingType(tokens[index]);

                index = findTitle("SalePrice");
                double salePrice = Double.Parse(tokens[index]);

                index = findTitle("MSSubClass");
                int subClass = int.Parse(tokens[index]);
                List<Double> output = new List<double>(new double[16]);
                output[MSSubClassValue.IndexOf(subClass)] = 1;

                if (i > whereToSplit)
                {
                    //dodaj u train
                    trainY.Add( output );
                    train.Add(new List<double>() { neighborhood, lotArea, bldgType, salePrice });
                }
                else
                {
                    //dodaj u test
                    testY.Add( output );
                    test.Add(new List<double>() { neighborhood, lotArea, bldgType, salePrice });
                }

            }

            normalize(train);
            normalizeTest(test);
        }

        public List<double> output(int subClass){
            List<double> ret = null;

            if(subClass == 20)
            {
                ret = new List<double>() { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            else if(subClass == 30)
            {
                ret = new List<double>() { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            else if (subClass == 40)
            {
                ret = new List<double>() { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            else if (subClass == 45)
            {
                ret = new List<double>() { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            else if (subClass == 50)
            {
                ret = new List<double>() { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            else if (subClass == 60)
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            else if (subClass == 70)
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            else if (subClass == 75)
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            else if (subClass == 80)
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };
            }
            else if (subClass == 85)
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 };
            }
            else if (subClass == 90)
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };
            }
            else if (subClass == 120)
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 };
            }
            else if (subClass == 150)
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 };
            }
            else if (subClass == 160)
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            }
            else if (subClass == 180)
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 };
            }
            else
            {
                ret = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            }

            return ret;
        }

        private double parseNeighborhood(String place)
        {
            if (neighbourhoods.ContainsKey(place) == false)
                neighbourhoods.Add(place, neighbourhoods.Count);

            return neighbourhoods[place];
        }

        private double parseBuildingType(String type)
        {
            if (buildingTypes.ContainsKey(type) == false)
                buildingTypes.Add(type, neighbourhoods.Count);

            return buildingTypes[type];
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

        private List<double> mins = new List<double>();
        private List<double> maxs = new List<double>();
        //normalizacija svih podataka -> svi se svode na [0,1]
        //formula: zi = (xi - min(x)) / (max(x) - min(x))
        //x - vektor svih vrednosti nekog ulaza (npr col_1 je x)
        //xi - vrednost i-tog elementa u vektor x
        //zi - normalizovana vrednost x
        public void normalize(List<List<double>> input)
        {
            //trazenje min i max za svaku kolonu
            //mins sadrzi minimum svake kolone, maxs maksimum           
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

        public void normalizeTest(List<List<double>> input)
        {
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
