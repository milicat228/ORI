using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        //TODO 2: Definisati obe vrste koraka
        public static int[,] lovac = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } };
        public static int[,] kralj = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        private bool skupljenoObavezno = false;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            //TODO 4: Dodati proveru za obavezno polje
            rez.skupljenoObavezno = this.skupljenoObavezno || (lavirint[markI, markJ] == 4);
            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 3: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu            
            List<State> rez = new List<State>();
            int[,] koraci = null;

            if( skupljenoObavezno == false)
            {
                koraci = lovac;
            }
            else
            {
                koraci = kralj;
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

            return rez;
        }

        public override int GetHashCode()
        {
            //TODO 5: Promeniti hash code da se moze vracati sa kutijom
            int key = 100 * markI + markJ;
            if (skupljenoObavezno == true)
                key += 10000;
            return key;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 6: Promeniti nacin provere da li je stanje krajnje
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && skupljenoObavezno == true;
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
