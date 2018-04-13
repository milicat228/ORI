using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        public static int[,] koraci = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        private List<int> odigrano = new List<int>(); //lista u kojoj se pamte odigrani potezi - posecena narandasta i plava polja
        private Mec mec = new Mec();

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;

            //TODO 3: Gledati polja
            //kopiraj mec
            rez.mec = new Mec(this.mec);
            //kopiraj posecena polja
            foreach (int item in this.odigrano)
            {
                rez.odigrano.Add(item);
            }

            //proveri da li je stao na plavo ili narandzasto polje (na koje nije stao u predthodnoj rundi
            //ovo ispod moze lepse, ali me mrzelo
            //cilj svega ovoga je da jedno polje moze vise puta da se koristi, samo ne uzastopno. 
            if (mec.daLiJeZavrsenMec() == false)
            {
                int vrednost = lavirint[markI, markJ];
                if (vrednost == 4 || vrednost == 5)
                {
                    if (rez.odigrano.Count == 0)
                    {
                        if (vrednost == 4)
                        {
                            rez.mec.plavoPolje();
                            rez.odigrano.Add(markI * 10 + markJ);
                        }
                        else
                        {
                            rez.mec.narandzasto();
                            rez.odigrano.Add(markI * 10 + markJ);
                        }
                    }
                    else if (rez.odigrano[rez.odigrano.Count - 1] != markI * 10 + markJ)
                    {
                        if (vrednost == 4)
                        {
                            rez.mec.plavoPolje();
                            rez.odigrano.Add(markI * 10 + markJ);
                        }
                        else
                        {
                            rez.mec.narandzasto();
                            rez.odigrano.Add(markI * 10 + markJ);
                        }
                    }
                }
            }

            return rez;
        }


        public List<State> mogucaSledecaStanja()
        {
            //TODO 2: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu          
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

        public String HashCodeString() 
        {
            //TODO 4: Promeniti Hashocode
            int kodI = 100 * markI + markJ;
            String kod = kodI.ToString() ;

            //dodaj sva odigrana polja
            foreach(int item in odigrano)
            {
                String i = item.ToString();
                kod = i + kod;
            }

            //Ja ne bih stavljala i brojeve poena, jer ce onda robot da razlikuje ako je stao na narandazto i prvi dobio poen i ako je stao na narandazto i ako je drugi dobio poen
            //Vrv ce barem deo nastimati sebi da se vrati na neko polje i da se mec pre zavrsi, pa nije zanimljivo

            //Nisam sigurna moze li samo broj poena da se doda u hash obicni
            //Ako se to uradi sigurno nece uvek naci optimalni put, jer ce mu biti isto do koje kutije je kada stigao, samo ce iskoristiti prvu koju nadje, a ta ne mora biti najbliza
            //posebno jer u A* juri do cilja i dok mec nije zavrsen.
            //Ima jos takvih verzija. Znaci ako bas ne pamtimo koju polje je birao, nece naci najbolje

            return kod;
        }

        public bool isKrajnjeStanje()
        {
            //TODO 7: Promeniti krajnje stanje
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && mec.daLiJeZavrsenMec() == true;
        }

        public List<State> path()
        {
            List<State> putanja = new List<State>(); //TODO 8: Ja sam ovde stavljala break point ako zelim da vidim kako su se dodeljivali poeni
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
