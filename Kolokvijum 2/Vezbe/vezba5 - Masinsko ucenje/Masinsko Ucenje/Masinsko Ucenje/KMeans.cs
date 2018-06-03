using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masinsko_Ucenje
{
    public class KMeans
    {
        public List<Point> elementi = new List<Point>();
        public List<Cluster> grupe = new List<Cluster>();
        public int brojGrupa = 0;
        Random rnd = new Random();

        public void podeliUGrupe(int brojGrupa, double errT)
        {
            this.brojGrupa = brojGrupa;
            if (brojGrupa == 0) return;
            //------------  inicijalizacija -------------
            for (int i = 0; i < brojGrupa; i++)
            {
                // TODO 5: na slucajan nacin inicijalizovati centre grupa
                int indeks = rnd.Next(0, elementi.Count);
                Cluster cluster = new Cluster();
                cluster.centar = new Point(elementi[indeks].x, elementi[indeks].y);
                grupe.Add(cluster);
            }

            //------------- iterativno racunanje centara ---
            for (int it = 0; it < 1000; it++)
            {
                foreach (Cluster grupa in grupe)
                    grupa.elementi = new List<Point>();

                foreach (Point cc in elementi)
                {
                    int najblizaGrupa = 0;
                    for (int i = 0; i < brojGrupa; i++)
                    {
                        if (grupe[najblizaGrupa].rastojanje(cc) >
                            grupe[i].rastojanje(cc))
                        {
                            najblizaGrupa = i;
                        }
                    }
                    grupe[najblizaGrupa].elementi.Add(cc);
                }
                double err = 0;
                for (int i = 0; i < brojGrupa; i++)
                {
                    err += grupe[i].pomeriCentar();
                }
                if (err < errT)
                    break;

                Main.clusteringHistory.Add(Main.DeepClone(this.grupe));
            }
        }
    }

    [Serializable]
    public class Cluster
    {
        public Point centar = new Point(0,0);
        public List<Point> elementi = new List<Point>();

        public double rastojanje(Point c)
        {   // TODO 6: implementirati funkciju rastojanja
            return  Math.Sqrt( Math.Pow(c.x - centar.x,2) + Math.Pow(c.y - centar.y,2) );
        }

        public double pomeriCentar()
        {   // TODO 7: implemenitrati funkciju koja pomera centre klastera
            Point stariCentar = new Point(centar.x, centar.y);
            double sX = 0;
            double sY = 0;

            foreach(Point p in elementi)
            {
                sX += p.x;
                sY += p.y;
            }

            centar.x = sX / elementi.Count;
            centar.y = sY / elementi.Count;

            double retVal = rastojanje(stariCentar);
            return retVal;
        }
    }
}
