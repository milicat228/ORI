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
        private List<int> indexs = new List<int>(); //lista indeksa kolona koje ce se koristiti
        private String[] lines;

        //prosledjuje se lista naziva kolona koje treba iskoristiti
        public List<Element> readFile(String[] titlesToUse)
        {           
            lines = File.ReadAllLines(@"./../../dataset.csv");
            String[] titles = lines[0].Split(',');
            lines = lines.Skip(1).ToArray();

            //pronadji naslove
            foreach(String title in titlesToUse)
            {
                for(int i = 0; i < titles.Length; ++i)
                {
                    if (title.Equals(titles[i]))
                    {
                        indexs.Add(i);
                        break;
                    }
                }
            }

            return getElemente();
        }


        public List<Element> getElemente()
        {
            List<Element> ret = new List<Element>();

            foreach(String line in lines)
            {
                String[] tokens = line.Split(',');
                Element e = new Element();
                foreach(int index in indexs)
                {                    
                    double value = Double.Parse(tokens[index]);
                    e.getKoordinate().Add(value);
                }
                
                //ovo je jer se gledaju samo likovi iz barem 3 knjige
                if( e.getKoordinate().Sum() >= 3)
                     ret.Add(e);
            }

            return ret;
        }


    }
}
