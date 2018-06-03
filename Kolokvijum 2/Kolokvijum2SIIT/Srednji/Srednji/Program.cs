using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Srednji
{
    class Program
    {
        static void Main(string[] args)
        {
            KMeans kMeans = new KMeans();
          
            int brojGrupa = 3;
            double errT = 0.001;

            Utils utils = new Utils();
            List<Element> elements = utils.readFile();            
            Console.WriteLine("Učitano {0} elemenata.", elements.Count);

            Console.WriteLine("Započeto klasterovanje.");
            kMeans.elementi = elements;
            kMeans.PodeliUGrupe(brojGrupa, errT);
            Console.WriteLine("Završeno klasterovanje.");
            Console.WriteLine();
            Console.WriteLine();


            for (int i = 0; i < brojGrupa; ++i)
            {
                Cluster c = kMeans.grupe[i];
                Console.WriteLine("Klaster {0}", i);
                Console.WriteLine("Ukupno elemenata: {0}", c.elementi.Count);

                int[] tip = { 0, 0, 0 };

                foreach (Element e in c.elementi)
                {
                    int which = utils.tipElementa[e];
                    ++tip[which];
                }

                Console.WriteLine("tip_1:{0}, tip_2:{1}, tip_3:{2}", tip[0], tip[1], tip[2]);               
                Console.WriteLine("U ovom klasteru su elemeti tipa {0}", Array.IndexOf(tip, tip.Max()) + 1);
                Console.WriteLine();
                Console.WriteLine();
            }

            List<Element> test = utils.readTest();
            foreach(Element e in test)
            {
                int index = kMeans.razvrstajNovi(e);
                Console.WriteLine("Element tipa {0} pripada klasteru {1}", utils.tipElementa[e] + 1, index);
            }
            

            Console.ReadLine();
        }
    }
}
