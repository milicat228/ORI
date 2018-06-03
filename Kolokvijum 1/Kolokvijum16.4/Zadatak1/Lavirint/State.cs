using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        public static int[,] kraljica = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        private static int[,] skakac = { { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 } };
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        private bool zutaKutija = false;
        private int pesak = 0;
        private List<int> pokupljenePlave = new List<int>();
        private int plaviPoeni = 0;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;

            //TODO 2: Skupiti zutu kutiju
            rez.zutaKutija = this.zutaKutija || (lavirint[markI, markJ] == 5);

            //TODO 7: Plave kutije
            rez.plaviPoeni = this.plaviPoeni;
            foreach(int item in this.pokupljenePlave)
            {
                rez.pokupljenePlave.Add(item);
            }
            if (lavirint[markI, markJ] == 4 && rez.pokupljenePlave.Contains(markI * 10 + markJ) == false)
            {
                rez.pokupljenePlave.Add(markI * 10 + markJ);
                ++rez.plaviPoeni;
            }
            
            //TODO 6: Pesak
            if (lavirint[markI, markJ] == 6)
            {
                rez.pesak = this.pesak + 1;
                if (rez.plaviPoeni > 0)
                {
                    rez.pesak = rez.pesak - 1;
                    rez.plaviPoeni = rez.plaviPoeni - 1;
                }
            }
            else
            {
                rez.pesak = 0;
            }


            return rez;
        }

        
        public List<State> mogucaSledecaStanja()
        {
            //TODO 3: Kretanje
            List<State> rez = new List<State>();

            int[,] koraci = null;
            bool jedanKorak = false;

            if( zutaKutija == true)
            {
                koraci = kraljica;
            }
            else
            {
                koraci = skakac;
                jedanKorak = true;
            }

            for (int i = 0; i < koraci.GetLength(0); ++i)
            {
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
                    //proveriti da li je figura stala tri puta na pesak
                    if (novi.pesak > 3)
                        continue;

                    rez.Add(novi);

                    //kralj moze da prelazi samo po jedno polje u jednom koraku, pa za njega odmah napustamo petlju
                    if (jedanKorak == true)
                        break;
                }

            }

            return rez;
        }

        public override int GetHashCode()
        {
            //TODO 4: Promeniti hash kod
            int key = 10 * markI + markJ;
            if (zutaKutija == true)
                key += 100;

            int i = 1000;
            foreach (int item in pokupljenePlave)
            {
                key += item * i;
                i *= 100;
            }

            return key;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 5: Promena krajnjeg stanja
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && zutaKutija == true;
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
