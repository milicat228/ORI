using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        private static int[,] konj = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } };
        private static int[,] ograniceniTop = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        private int poeni = 0;
        private List<int> skupljeneKutije = new List<int>();

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;

            rez.poeni = this.poeni;
            //TODO 3: Skupljanje kutija/poena
            foreach(int item in this.skupljeneKutije)
            {
                rez.skupljeneKutije.Add(item);
            }

            if( lavirint[markI, markJ] == 4 && skupljeneKutije.Contains(markI * 10 + markJ) == false)
            {
                //nadjena nova plava kutija
                rez.poeni++;
                rez.skupljeneKutije.Add(markI * 10 + markJ);
            }

            if (lavirint[markI, markJ] == 5 && skupljeneKutije.Contains(markI * 10 + markJ) == false)
            {
                //nadjena nova zuta kutija
                rez.poeni += 3;
                rez.skupljeneKutije.Add(markI * 10 + markJ);
            }

            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 2: Implementirati kretanje
            List<State> rez = new List<State>();

            int[,] koraci = null;
            bool oneStep = false;

            if (poeni < 6)
            {
                //levo
                koraci = konj;
                oneStep = true;
            }
            else
            {
                //desno
                koraci = ograniceniTop;
            }

            for (int i = 0; i < koraci.GetLength(0); ++i)
            {
                int j = 1;
                while (j < 2) //zbog ogranicenja za topa
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
                    //za figure koje prelaze samo jedno polje po potezu
                    if (oneStep == true)
                        break;
                }
            }

            return rez;
        }

        public override int GetHashCode()
        {
            //TODO 4: Promeniti hashcode da se moze vracati sa kutijama
            int key = 10 * markI + markJ;
            int i = 100;
            foreach(int item in skupljeneKutije)
            {
                key += item * i;
                i *= 100;
            }
            return key;
        }

        public bool isKrajnjeStanje()
        {
            if (skupljeneKutije.Count > 0)
            {
                bool basic = Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && poeni >= 11;
                //provera da li je zadnja plava
                int zadnja = skupljeneKutije[skupljeneKutije.Count - 1];
                int j = zadnja % 10;
                int i = zadnja / 10;
                if (lavirint[i, j] == 4)
                    return basic;
                else
                    return false;
            }
            else
                return false;            
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
