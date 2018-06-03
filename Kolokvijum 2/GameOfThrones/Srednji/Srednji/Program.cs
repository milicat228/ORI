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
            String[] titles = { "book1", "book2", "book3", "book4", "book5" };
            Console.WriteLine("Započeto čitanje fajla.");
            List<Element> elements = utils.readFile(titles);            
            Console.WriteLine("Učitano {0} elemenata.", elements.Count);

            Console.WriteLine("Započeto klasterovanje.");
            kMeans.elementi = elements;
            kMeans.PodeliUGrupe(brojGrupa, errT);
            Console.WriteLine("Završeno klasterovanje.");
                       
            for(int i = 0; i < brojGrupa; ++i)
            {
                Cluster c = kMeans.grupe[i];
                Console.WriteLine("Klaster {0}", i);
                Console.WriteLine("Ukupno likova: {0}", c.elementi.Count);
                List<Element> sveKnjige = c.elementi.Where(e => e.getKoordinate().Sum() == 5).ToList();
                List<Element> Knjige4 = c.elementi.Where(e => e.getKoordinate().Sum() == 4).ToList();
                List<Element> Knjige3 = c.elementi.Where(e => e.getKoordinate().Sum() == 3).ToList();
                Console.WriteLine("Likova iz svih 5 knjiga: {0}", sveKnjige.Count);
                Console.WriteLine("Likova iz 4 knjiga: {0}", Knjige4.Count);
                Console.WriteLine("Likova iz 3 knjige: {0}", Knjige3.Count);
                if (c.elementi.Count != 0)
                     Console.WriteLine("Procentualno:{0}%", sveKnjige.Count * 100 / c.elementi.Count);
            }

            Console.ReadLine();
        }
    }
}
