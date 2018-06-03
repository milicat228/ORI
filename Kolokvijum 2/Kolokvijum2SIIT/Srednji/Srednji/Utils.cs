using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Srednji
{
    public class Utils
    {

        private String[] lines;
        //ovo sluzi za proveru kasnije
        public Dictionary<Element, int> tipElementa = new Dictionary<Element, int>();

        //prosledjuje se lista naziva kolona koje treba iskoristiti
        public List<Element> readFile()
        {
            lines = File.ReadAllLines(@"./../../train.csv");
            lines = lines.Skip(1).ToArray();
            
            //napuni listu
            List<Element> ret = new List<Element>();
            foreach(String line in lines)
            {
                ret.Add(parseLine(line));
            }

            normalize(ret);

            return ret;
        }

        public List<Element> readTest()
        {
            lines = File.ReadAllLines(@"./../../test.csv");
            lines = lines.Skip(1).ToArray();

            //napuni listu
            List<Element> ret = new List<Element>();
            foreach (String line in lines)
            {
                ret.Add(parseLine(line));
            }

            normalize(ret);

            return ret;
        }


        private Element parseLine(String line)
        {
            String[] tokens = line.Split(',');

            //parsiraj ulaz
            double col_1 = Double.Parse(tokens[1]);
            double col_2 = Double.Parse(tokens[2]);
            double col_3 = Double.Parse(tokens[3]);
            double col_4 = Double.Parse(tokens[4]);

            Element e = new Element(new List<double>() { col_1, col_2, col_3, col_4 });

            String type = tokens[5];
            if (type.Equals("type_1"))
            {
                tipElementa.Add(e, 0);
            }
            else if (type.Equals("type_2"))
            {
                tipElementa.Add(e, 1);
            }
            else
            {
                tipElementa.Add(e, 2);
            }
          
            return e;
        }

        //normalizacija svih podataka -> svi se svode na [0,1]
        //formula: zi = (xi - min(x)) / (max(x) - min(x))
        //x - vektor svih vrednosti nekog ulaza (npr col_1 je x)
        //xi - vrednost i-tog elementa u vektor x
        //zi - normalizovana vrednost x
        public void normalize(List<Element> input)
        {
            //trazenje min i max za svaku kolonu
            //mins sadrzi minimum svake kolone, maxs maksimum
            List<double> mins = new List<double>();
            List<double> maxs = new List<double>();
            for (int i = 0; i < input[0].getKoordinate().Count; ++i)
            {
                mins.Add(input[0].getKoordinate()[i]);
                maxs.Add(input[0].getKoordinate()[i]);
            }

            for (int i = 1; i < input.Count; ++i)
            {
                for (int j = 0; j < input[i].getKoordinate().Count; ++j)
                {
                    if (input[i].getKoordinate()[j] < mins[j])
                    {
                        mins[j] = input[i].getKoordinate()[j];
                    }

                    if (input[i].getKoordinate()[j] > maxs[j])
                    {
                        maxs[j] = input[i].getKoordinate()[j];
                    }
                }
            }

            //normalizacija vrednosti
            for (int i = 0; i < input.Count; ++i)
            {
                for (int j = 0; j < input[i].getKoordinate().Count; ++j)
                {
                    input[i].getKoordinate()[j] = (input[i].getKoordinate()[j] - mins[j]) / (maxs[j] - mins[j]);
                }
            }
        }

    }
}
