using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        //TODO 3.1: Definisati nacine kretanja
        public static int[,] lovac = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } };
        public static int[,] kralj = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;

        public bool skupioVodu = false;
        private List<int> ugaseneVatre = new List<int>();

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;

            //TODO 4: Da li je skupio vodu
            //proveriti da li je skupio vodu
            rez.skupioVodu = this.skupioVodu || (lavirint[markI, markJ] == 4);

            //TODO 5: Gasenje vatre
            if( rez.skupioVodu == true )
            {
                //preuzmi ugasene vatre
                foreach(int item in this.ugaseneVatre)
                {
                    rez.ugaseneVatre.Add(item);
                }

                //ugasio novu ako je nadjena
                if( lavirint[markI, markJ] == 5 && rez.ugaseneVatre.Contains(markI*10 + markJ) == false)
                {
                    rez.ugaseneVatre.Add(markI * 10 + markJ);
                }
            }

            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 3.2: Implementirati kretanje
            List<State> rez = new List<State>();
            int[,] koraci = null;
            bool kreceSeKaoKralj = false;

            if (skupioVodu == true)
            {
                koraci = kralj;
                kreceSeKaoKralj = true;
            }
            else
            {
                koraci = lovac;
            }

            for (int i = 0; i < koraci.GetLength(0); ++i)
            {

                //lovac moze da u jednom potezu prelazi bilo koji broj polja dijagonalno, sve dok ne dodje do zida ili granice lavirinta
                //isto vazi i za topa
                int j = 1; //koliko dijagonalnih koraka treba da napravi
                while (true)
                {
                    int nextI = markI + j * koraci[i, 0];
                    int nextJ = markJ + j * koraci[i, 1];
                    ++j;

                    //proveri da li je polje validno, tj. u granicama lavirinta
                    if (nextI < 0 || nextI >= Main.brojKolona || nextJ < 0 || nextJ >= Main.brojVrsta)
                    {
                        break; //izasli smo iz granica lavirinta
                    }

                    //proveriti da li je polje sivo - ne moze se preci na njega
                    if (lavirint[nextI, nextJ] == 1)
                    {
                        break; //polje je sivo
                    }

                    //dodaje se novo stanje
                    State novi = sledeceStanje(nextI, nextJ);
                    rez.Add(novi);

                    //kralj moze da prelazi samo po jedno polje u jednom koraku, pa za njega odmah napustamo petlju
                    if (kreceSeKaoKralj == true)
                        break;
                }

            }

            return rez;
        }

        public override int GetHashCode()
        {
            //TODO 7: Promeniti hash code
            int key = 10 * markI + markJ;

            //rezervaor
            if (skupioVodu == true)
                key += 100;

            int shift = 1000;
            foreach(int item in ugaseneVatre)
            {
                key += item * shift;
                shift *= 100;
            }

            return key;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 6: Promeniti krajnje stanje
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && Main.vatre.Count == ugaseneVatre.Count;
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
