using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        //TODO 1: Robot se krece kao sahovska figura konj
        private static int[,] koraci = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } };
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        private bool kutijaPokupljena;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            //TODO 8.1: Da li je kutija skupljena
            rez.kutijaPokupljena = this.kutijaPokupljena;
            //proveri da li je trenutno polje kutija
            if(lavirint[markI,markJ] == 4)
            {
                rez.kutijaPokupljena = true;
            }
            return rez;
        }


        public List<State> mogucaSledecaStanja()
        {
            //TODO 2: Implementirati metodu tako da odredjuje moguca sledeca stanja
            List<State> mogucaStanja = new List<State>();

            for (int i = 0; i < koraci.GetLength(0); ++i)
            {
                int novoI = markI + koraci[i, 0];
                int novoJ = markJ + koraci[i, 1];

                //provera koordinata
                if (novoI < 0 || novoI >= Main.brojVrsta)
                    continue;
                if (novoJ < 0 || novoJ >= Main.brojKolona)
                    continue;

                //stanje nije moguce ako je siva kutija
                if (lavirint[novoI, novoJ] == 1)
                    continue;

                //stanje se dodaje na listu mogucih
                mogucaStanja.Add(sledeceStanje(novoI, novoJ));

            }

            return mogucaStanja;
        }

        public override int GetHashCode()
        {
            //TODO 8.2: Sme se vracati preko polja kada pokupi kutiju
            int key = 100 * markI + markJ;
            if(kutijaPokupljena == true)
            {
                key = 1000 + key;
            }
            return key;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 8.3: Stanje je konacno samo ako je pokupljena kutija
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && kutijaPokupljena;
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
