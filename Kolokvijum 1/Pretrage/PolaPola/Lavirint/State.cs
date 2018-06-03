using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        private static int[,] konj = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } };
        private static int[,] top = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        
        private List<int> plaveKutije = new List<int>(); //cuva se kutija kao 10 * x_koordinata + y_koordinata
        private List<int> narandzasteKutije = new List<int>();
        public static int potrebnoPlavih = 2;
        public static int potrebnoNarandastih = 3;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;

            //TODO 3: Implementirati skupljanje kutija

            //preuzimanje plavih kutija prethodnog stanja
            foreach (int polje in this.plaveKutije)
            {
                rez.plaveKutije.Add(polje);
            }
            //provera da li je trenutno polje nova plava kutija
            if (lavirint[markI, markJ] == 4 && rez.plaveKutije.Contains(10 * markI + markJ) == false)
            {
                rez.plaveKutije.Add(10 * markI + markJ);
            }

            //narandaste kutije se skupljaju tek posle plavih u ovoj varijanti, za skupljanje bez specijalnog reda skloniti if
            if (rez.plaveKutije.Count >= potrebnoPlavih)
            {
                //preuzimanje narandzastih
                foreach (int polje in this.narandzasteKutije)
                {
                    rez.narandzasteKutije.Add(polje);
                }
                //provera da li je trenutno polje nova narandzasta kutija
                if (lavirint[markI, markJ] == 5 && rez.narandzasteKutije.Contains(10 * markI + markJ) == false)
                {
                    rez.narandzasteKutije.Add(10 * markI + markJ);
                }
            }

            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 2: Implementirati kretanje
            List<State> rez = new List<State>();

            int[,] koraci = null;
            bool oneStep = false;

            if( markJ <= 4)
            {
                //levo
                koraci = konj;
                oneStep = true;
            }
            else
            {
                //desno
                koraci = top;
            }

            for (int i = 0; i < koraci.GetLength(0); ++i)
            {
                int j = 1;
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
                    //za figure koje prelaze samo jedno polje po potezu
                    if (oneStep == true)
                        break;

                    //ako je top i presao je levo, staje
                    if (oneStep == false && nextJ <= 4)
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

            //oznacavanje kutija koje su koristene
            foreach (int polje in this.plaveKutije)
            {
                key += polje * i;
                i *= 100;
            }
            foreach (int polje in this.narandzasteKutije)
            {
                key += polje * i;
                i *= 100;
            }

            return key;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 5: Prilagoditi krajnje stanje

            if (plaveKutije.Count < potrebnoPlavih)
                return false;
            if (narandzasteKutije.Count < potrebnoNarandastih)
                return false;

            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ;
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
