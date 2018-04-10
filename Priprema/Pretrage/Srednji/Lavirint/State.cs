using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Lavirint
{
    public class State
    {
        public static int[,] koraci = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        private bool plavaKutija;
        private bool narandzastaKutija;
        public bool izasaoIzPortala = false; //blokira da robot kada izadje iz portala, odmah prodje nazad kroz taj portal

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;

            //TODO 4: Implementirati proveru da li je pokupljena kutija

            //skupljanje plave kutije
            rez.plavaKutija = this.plavaKutija || (lavirint[rez.markI, rez.markJ] == 4);

            //narandzasta se skuplja samo ako postoji vec plava
            if( rez.plavaKutija == true)
            {
                rez.narandzastaKutija = this.narandzastaKutija || (lavirint[rez.markI, rez.markJ] == 5);
            }
            else
            {
                rez.narandzastaKutija = false;
            }


            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 3: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu  
            //Ja cu tako da moze da bira hoce li da prodje kroz portal. Tj. kada je na portalu moze i samo da ga predje
            List<State> rez = new List<State>();

            //normalna stanja (samo kretanje)
            for (int i = 0; i < koraci.GetLength(0); ++i)
            {
                int nextI = markI + koraci[i, 0];
                int nextJ = markJ + koraci[i, 1];

                //proveri da li je polje validno, tj. u granicama lavirinta
                if (nextI < 0 || nextI >= Main.brojKolona || nextJ < 0 || nextJ >= Main.brojVrsta)
                {
                    //polje nije validno, pa se preskace
                    continue;
                }

                //proveriti da li je polje sivo - ne moze se preci na njega
                if (lavirint[nextI, nextJ] == 1)
                {
                    //polje je sivo, pa se preskace
                    continue;
                }

                //dodaje se novo stanje
                State novi = sledeceStanje(markI + koraci[i, 0], markJ + koraci[i, 1]);
                novi.izasaoIzPortala = false;
                rez.Add(novi);
            }

            //prolazak kroz portal
            if( lavirint[markI, markJ] == 6 && izasaoIzPortala == false)
            {
                foreach( Point point in Main.portali) //moguce sledece stanje su svi portali osim tog
                {
                    if (markI == point.X && markJ == point.Y) //preskoci taj portal kroz koji je usao
                        continue;
                    State novoStanje = sledeceStanje(point.X, point.Y);
                    novoStanje.izasaoIzPortala = true;
                    rez.Add(novoStanje);
                }
            }

            return rez;
        }

        public override int GetHashCode()
        {
            //TODO 5: krajnje stanje je samo ako su kutije poklupljene
            int key = 100 * markI + markJ;

            if (plavaKutija == true)
                key += 10000;

            if (narandzastaKutija == true)
                key += 100000;

            return key;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 6: Izmeniti proveru da li je stanje krajnje
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && plavaKutija == true && narandzastaKutija == true;
        }

        public List<State> path()
        {
            List<State> putanja = new List<State>();
            State tt = this;
            while (tt != null)
            {
                putanja.Insert(0, tt);
                tt = tt.parent;
            }
            return putanja;
        }

        
    }
}
