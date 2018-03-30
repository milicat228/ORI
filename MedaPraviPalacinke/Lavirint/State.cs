using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Lavirint
{
    public class State
    {
        //TODO 2.1: Definisati moguce korake, Meda moze da se krece i dijagonalno
        private static int[,] steps = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 }, { 1, 1 }, { -1, -1 }, { 1, -1 }, { -1, 1 } };
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        //TODO 3.2: Dodati podatak koji modeluje posecena obavezna polja
        private Dictionary<Point, bool> reqStates = new Dictionary<Point, bool>();

        public State()
        {
            //TODO 3.3: Na pocetku ni jedno obavezno polje nije poseceno
            foreach(Point point in Main.obaveznaStanja)
            {
                reqStates.Add(point, false);
            }
        }

        public State sledeceStanje(int markI, int markJ)
        {            
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            //TODO 3.4: Preuzeti koja obavezna polja je Meda posetio do sada i proveriti da li je sada na novom obaveznom polju
            //preuzimanje zatecenog
            foreach (KeyValuePair<Point, bool> entry in this.reqStates)
            {
                rez.reqStates[entry.Key] = entry.Value;
            }
            //provera za trenutno
            if( lavirint[markI,markJ] == 4)
            {
                rez.reqStates[new Point(markI,markJ)] = true;     
            }

            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 2.2: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu
            //TODO 2.3: Prosiriti metodu tako da se ne moze prolaziti kroz sive kutije
            List<State> rez = new List<State>();

            for(int i = 0; i < steps.GetLength(0); ++i)
            {
                int nextI = markI + steps[i, 0];
                int nextJ = markJ + steps[i, 1];

                //proveri da li je polje validno, tj. u granicama lavirinta
                if( nextI < 0 || nextI >= Main.brojKolona || nextJ < 0 || nextJ >= Main.brojVrsta)
                {
                    //polje nije validno, pa se preskace
                    continue;
                }

                //proveriti da li je polje sivo - ne moze se preci na njega
                if( lavirint[nextI,nextJ] == 1)
                {
                    //polje je sivo, pa se preskace
                    continue;
                }

                //dodaje se novo stanje
                State novi = sledeceStanje(markI + steps[i, 0], markJ + steps[i, 1]);
                rez.Add(novi);

            }

            return rez;
        }

        public override int GetHashCode()
        {
            //TODO 3.6: Promeniti tako da hash code zavisi od broja pokupljenih obaveznih polja
            int i = 1000;
            int key = 100 * markI + markJ;
            foreach (KeyValuePair<Point, bool> entry in this.reqStates)
            {
                if( entry.Value == true)
                {
                    key = key + i;
                }
                i *= 10;
            }
            return key;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 3.5: Promeniti kako se proverava da li je stanje krajnje
            //da bi stanje moglo biti krajnje, mora biti palacinka stanje
            if (Main.krajnjeStanje.markI != markI || Main.krajnjeStanje.markJ != markJ)
            {
                return false; //nismo na palacinka polju => stanje nije krajnje
            }

            //stanje je krajnje samo ako je Meda posetio sva obavezna polja
            foreach (KeyValuePair<Point, bool> entry in this.reqStates)
            {
                if( this.reqStates[entry.Key] == false)
                {
                    return false; //nismo pokupili neke sastojke => stanje nije krajnje
                }
                
            }

            return true; //pokupili smo sve sastoje i stigli do palacinka polja
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
