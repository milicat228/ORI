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

            return rez;            
        }

        
        public List<State> mogucaSledecaStanja()
        {
            List<State> rez = new List<State>();           
            //TODO 3: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu            

            int[,] koraci = null;
            bool kreceSeKaoKralj = false;

            if (posedujeDzip == true)
            {
                koraci = top;
            }
            else if (skupljenoObavezno == true)
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
            //TODO 5: Promeniti hash code da se moze vracati sa kutijom
            int key = 100 * markI + markJ;
            if (skupljenoObavezno == true)
                key += 10000;
            if (posedujeDzip == true)
                key += 100000;
           
            return key;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 6: Promeniti nacin provere da li je stanje krajnje
            return  Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && skupljenoObavezno == true;
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
