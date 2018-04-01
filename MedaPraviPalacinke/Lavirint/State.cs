using System;
using System.Collections;
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
        private Hashtable visitedFields = new Hashtable(); //posecena obavezna polja se upisuju kao x_koordinata*10 + y_koordinata
                

        public State sledeceStanje(int markI, int markJ)
        {            
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;

            //TODO 3.3: Preuzeti koja obavezna polja je Meda posetio do sada i proveriti da li je sada na novom obaveznom polju
            //preuzimanje zatecenog
            foreach (DictionaryEntry hash in this.visitedFields)
            {
                rez.visitedFields.Add(hash.Key,null);            
            }

            //provera za trenutno
            if( lavirint[markI,markJ] == 4 && visitedFields.ContainsKey(markI * 10 + markJ) == false) //more biti obavezno i ne sme biti prethodno poseceno
            {
                rez.visitedFields.Add(markI*10 + markJ, null);
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
            int key = 10 * markI + markJ; //dve najnize cifre predstavljaju indekse polja na kome se Meda trenutno nalazi
            //to je donjih 7 bita. Počevši od 8og bita svaki bit predstavlja da li je poseceno obavezno polje

            int i = 128; //binarno 1000 0000 (oznaka da je skupljena prva kutija)
            
            foreach (Point point in Main.obaveznaStanja) //iterira se kroz listu svih obaveznih
            {
                if( visitedFields.ContainsKey( point.X * 10 + point.Y) == true ) //provera da li je trenutno obavezno polje poseceno
                {
                    key = key | i; //1 se upisuje na bit koji označava da je to obavezno polje poseceno
                }

                i = i<<1; //shift za jedno mesto u levo. npr: 1000 0000 postaje 1 0000 0000 i tako se dobije sledeća obavezno polje
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
            foreach (Point point in Main.obaveznaStanja)
            {
                if( visitedFields.ContainsKey(point.X * 10 + point.Y ) == false ) //ako nije poseceno neko obavezno polje, stanje nije konacno
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
