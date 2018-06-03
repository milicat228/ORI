using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Srednji
{
    public class Cluster
    {
        public Element centar = new Element();
        public List<Element> elementi = new List<Element>();

        public double pomeriCentar()
        {  
            Element stariCentar = centar;

            //novi cenatr se pomera na teziste elemenata koji mu pripadaju
            //teziste se racuna za svaku koordinatu
            //sabere se ta koordinata svakog elementa, a zatim se deli sa brojem elemenata
            centar = new Element(stariCentar.getKoordinate().Count);
            foreach (Element e in elementi)
            {
                for(int i = 0; i < e.getKoordinate().Count; ++i)
                {
                    centar.getKoordinate()[i] += e.getKoordinate()[i];
                }
            }

            
            for (int i = 0; i < centar.getKoordinate().Count; ++i)
            {
                centar.getKoordinate()[i] /= elementi.Count;
            }            
                       
            return Element.udaljenost(stariCentar, centar);
        }
    }
}
