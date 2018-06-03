using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Srednji
{
    public class KMeans
    {
        public List<Element> elementi = new List<Element>();
        public List<Cluster> grupe = new List<Cluster>();       
        Random rnd = new Random();

        public int razvrstajNovi(Element e)
        {
            int najblizaGrupa = 0;
            for (int i = 1; i < grupe.Count; i++)
            {
                if (Element.udaljenost(e, grupe[najblizaGrupa].centar) > Element.udaljenost(e, grupe[i].centar))
                {
                    najblizaGrupa = i;
                }
            }

            return najblizaGrupa;
        }

        public void PodeliUGrupe(int brojGrupa, double errT)
        {
            if (brojGrupa == 0) return;
            //------------  inicijalizacija -------------
            for (int i = 0; i < brojGrupa; i++)
            {                
                int indeks = rnd.Next(0, elementi.Count);
                Cluster cluster = new Cluster();
                cluster.centar = new Element(elementi[i]);                
                grupe.Add(cluster);
            }
            


            //------------- iterativno racunanje centara ---
            for (int it = 0; it < 1000; it++)
            {
                foreach (Cluster grupa in grupe)
                {
                    grupa.elementi = new List<Element>();                   
                }
                    
                

                foreach (Element e in elementi)
                {
                    int najblizaGrupa = 0;
                    for (int i = 1; i < brojGrupa; i++)
                    {                       
                        if (Element.udaljenost(e,grupe[najblizaGrupa].centar) > Element.udaljenost(e, grupe[i].centar))
                        {
                            najblizaGrupa = i;
                        }
                    }                    
                    grupe[najblizaGrupa].elementi.Add(e);
                }

                double err = 0;
                for (int i = 0; i < brojGrupa; i++)
                {
                    err += grupe[i].pomeriCentar();
                }
                if (err < errT)
                    break;                
            }
            }
        }
  
}
