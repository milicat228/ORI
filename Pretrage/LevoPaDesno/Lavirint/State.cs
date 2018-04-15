using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        public static int[,] koraci = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        private List<int> skupljenaPolja = new List<int>();

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;

            //TODO 5: Implementirati skupljanje kutija
            //preuzmi stare
            foreach (int item in this.skupljenaPolja)
            {
                rez.skupljenaPolja.Add(item);
            }
            //proveri da li je novo polje obavezno
            if (lavirint[markI, markJ] == 4)
            {
                if (markJ <= Main.pocetnoStanje.markJ) //obavezno polje je na levoj strani
                {
                    //skupi ga, ako je novo
                    if (rez.skupljenaPolja.Contains(markI * 10 + markJ) == false)
                    {
                        rez.skupljenaPolja.Add(markI * 10 + markJ);
                    }
                }
                else //obavezno polje je na desnoj strani
                {
                    //proveri da li su skupljena sva na levoj strani
                    if (rez.skupljenaPolja.Count >= Main.levo)
                    {
                        //ako jesu skupljena, skupi polje ako je novo
                        if (rez.skupljenaPolja.Contains(markI * 10 + markJ) == false)
                        {
                            rez.skupljenaPolja.Add(markI * 10 + markJ);
                        }
                    }
                }
            }


            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 2: Implementirati moguca sledeca stanja
            List<State> rez = new List<State>();
            for (int i = 0; i < koraci.GetLength(0); ++i)
            {               
                int nextI = markI + koraci[i, 0];
                int nextJ = markJ + koraci[i, 1];
                
                //proveri da li je polje validno, tj. u granicama lavirinta
                if (nextI < 0 || nextI >= Main.brojKolona || nextJ < 0 || nextJ >= Main.brojVrsta)
                {
                    continue; //izasli smo iz granica lavirinta
                }

                //proveriti da li je polje sivo - ne moze se preci na njega
                if (lavirint[nextI, nextJ] == 1)
                {
                    continue; //polje je sivo
                }

                //dodaje se novo stanje
                State novi = sledeceStanje(nextI, nextJ);
                rez.Add(novi);                             
            }
            return rez;
        }

        public override int GetHashCode()
        {
            //TODO 7: Promeniti nacin racunanja hash koda da obuhvati polja
            //napomena: ovaj nacin je ok za neki manji ukupan broj kutija, tipa 5 ili tako nesto. Ako moze da bude neograniceno kutija moze se koristiti string kao kod
            //kao u zadatku sa mecem
            int key = 10 * markI + markJ;
            int shift = 1000;
            foreach(int item in skupljenaPolja)
            {
                key += item * shift;
                shift *= 100;
            }

            return key;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 6: Promeniti nacin provere da li je stanje krajnje da bi se morala skupiti sva polja
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && skupljenaPolja.Count == Main.obaveznaPolja.Count;
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
