using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Srednji
{
    public class Element
    {
        private List<double> koordinate;

        public Element()
        {
            koordinate = new List<double>();
        }

        public Element(int brojKoordinata)
        {
            koordinate = new List<double>();
            for (int i = 0; i < brojKoordinata; ++i)
            {
                koordinate.Add(0.0);
            }
        }

        public Element(List<double> koordinate)
        {
            this.koordinate = koordinate;
        }

        public Element(Element e)
        {
            koordinate = new List<double>();
            for (int i = 0; i < e.getKoordinate().Count; ++i)
            {
                koordinate.Add(e.getKoordinate()[i]);
            }
        }

        public List<Double> getKoordinate()
        {
            return koordinate;
        }

        public static double udaljenost(Element e1, Element e2)
        {            
            return euklidskaUdaljenost(e1,e2);
            //return levenstajnUdaljenost(e1, e2);
            //return specijalnaUdaljenost(e1, e2);
        }

        //racuna udaljenost dva elementa
        //podrazumeva da imaju jednako koordinata, jer me mrzi da to proverim
        public static double euklidskaUdaljenost(Element e1, Element e2)
        {
            double distance = 0.0;

            for(int i = 0; i < e1.getKoordinate().Count; ++i)
            {
                distance += Math.Pow((e1.getKoordinate()[i] - e2.getKoordinate()[i]), 2);
            }
           
            //TODO: Radi se o jako malim brojevima, ne vaditi koren
            //distance = Math.Pow(distance, 1 / 2);

            return distance;
        }

        public static double specijalnaUdaljenost(Element e1, Element e2)
        {
            double ret = 0.0;
            ret = Math.Abs(e1.getKoordinate().Sum() - e2.getKoordinate().Sum());
            return ret;
        }

        //drugi nacin za racunanje udaljenosti      
        // radi lepo za reci, npr. moze da broji na koliko slova se razlikuju, ovde radi ocajno
        // https://www.youtube.com/watch?v=We3YDTzNXEk
        public static double levenstajnUdaljenost(Element e1, Element e2)
        {
           
            int n = e1.getKoordinate().Count;
            int m = e2.getKoordinate().Count;
            double[,] d = new double[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    double cost = (e1.getKoordinate()[j - 1] == e2.getKoordinate()[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),d[i - 1, j - 1]) + cost;
                }
            }
            // Step 7
            return d[n, m];
        }

    }
}
