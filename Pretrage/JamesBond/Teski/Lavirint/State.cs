using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        //TODO 2: Definisati sve vrste koraka
        public static int[,] lovac = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } };
        public static int[,] kralj = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        public static int[,] top = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        private bool skupljenoObavezno = false;
        private bool posedujeDzip = false;
        //TODO 8: Dodati promenljive koje modeluju da li je senzor ukljucen i da li su pritisnuti prekidaci
        private bool senzorUkljucen = false;
        private List<int> prekidaci = new List<int>();
        private bool stojSledeciKrug = false;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;

            //TODO 5: Promeniti racunanje troska zbog kanta (mocvara) polja
            if (lavirint[markI, markJ] == 5 && posedujeDzip == false)
            {
                rez.cost = this.cost + 2; //moze razlicito da se menja trosak u skladu s tim da li zelimo da idem blizim putem ili da vise zaobilazi kante
            }
            else
                rez.cost = this.cost + 1;

            //TODO 4: Dodati proveru za obavezno polje i dzip
            rez.skupljenoObavezno = this.skupljenoObavezno || (lavirint[markI, markJ] == 4);
            rez.posedujeDzip = this.posedujeDzip || (lavirint[markI, markJ] == 6);

            //TODO 8: Proveriti da li je ukljucen senzor
            if(Main.stanjaPodSenzorom.Contains(markI*10 + markJ) == true)
            {
                rez.senzorUkljucen = true;               
            }
            else
            {
                rez.senzorUkljucen = this.senzorUkljucen;
            }

            //TODO 9: Ako je senzor ukljucen, proveriti da li je dosao do novog prekidaca i preuzeti stare
            if(rez.senzorUkljucen == true)
            {
                for (int i = 0; i < this.prekidaci.Count; ++i)
                    rez.prekidaci.Add(this.prekidaci[i]);
            }

            if(rez.senzorUkljucen == true && rez.prekidaci.Contains(markI * 10 + markJ) == false && lavirint[markI,markJ] == 8)
            {
                rez.prekidaci.Add(markI * 10 + markJ);
            }            

            //TODO 10: Ako je pronasao dva prekidaca iskljuci senzore
            if( rez.prekidaci.Count >= 2)
            {
                rez.senzorUkljucen = false;
            }

            //TODO 11: Moze se kretati samo svake druge runde
            if( rez.senzorUkljucen == true)
            {
                stojSledeciKrug = !stojSledeciKrug;
            }

            return rez;            
        }

        
        public List<State> mogucaSledecaStanja()
        {
            List<State> rez = new List<State>();
            //TODO 12: Dodati da se krece svake druge runde, ako je ukljucen senzor
            if (!(stojSledeciKrug == true && senzorUkljucen == true))
            {
                //TODO 3: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu            

                int[,] koraci = null;

                if (posedujeDzip == true)
                {
                    koraci = top;
                }
                else if (skupljenoObavezno == true)
                {
                    koraci = kralj;
                }
                else
                {
                    koraci = lovac;
                }

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
                    State novi = sledeceStanje(nextI, nextJ);
                    rez.Add(novi);
                }

            }
            else
            {
                //ako stoji, znaci samo da je trenutno stanje moguce
                rez.Add(this);
            }

            return rez;
        }

        public override int GetHashCode()
        {           
            //TODO 5: Promeniti hash code da se moze vracati sa kutijom
            int key = 100 * markI + markJ;
            if (skupljenoObavezno == true)
                key += 10000;
            if (posedujeDzip == true)
                key += 100000;

            //TODO 13: Promeniti hash code tako da se prilagodi i senzoru i prekidacima
            if( senzorUkljucen == true )
                key += 1000000;

            int limit = 2 < prekidaci.Count ? 2 : prekidaci.Count;
            int s = 10000000;
            for (int i = 0; i < limit; ++i)
            {
                key += prekidaci[i] * s;
                s *= 100;
            }

            return key;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 6: Promeniti nacin provere da li je stanje krajnje
            bool kraj = Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && skupljenoObavezno == true;

            if (senzorUkljucen == false)
                return kraj;

            //TODO 14: Prosiriti za senzore
            if (senzorUkljucen == true && prekidaci.Count >= 2)
            {
                return kraj;
            }
            else
            {
                return false;
            }
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
