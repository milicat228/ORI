using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lavirint
{
    class AStarSearch
    {
        public State search(State pocetnoStanje)
        {
            //TODO 6: Implementirati A* pretragu
            //Vodjena pretraga
           
            List<State> stanjaNaObradi = new List<State>(); //uzima se najbolje resenje (po heuristici)          
            stanjaNaObradi.Add(pocetnoStanje);

            Hashtable posecenaStanja = new Hashtable(); //za pamcenja stanja (polja) u kojima je robot bio

            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = getBest(stanjaNaObradi);

                if (posecenaStanja.ContainsKey(naObradi.GetHashCode()) == false) //stanje se obradjuje samo ako nije obradjeno (novo je)
                {
                    Main.allSearchStates.Add(naObradi); //sluzi za prikaz u debug rezimu

                    if (naObradi.isKrajnjeStanje() == true) //provera da li je stanje krajnje
                    {
                        return naObradi;
                    }

                    //dodavanje u posecena stanja
                    posecenaStanja.Add(naObradi.GetHashCode(), null);

                    //sva moguca sledeca stanja se dodaju na pocetak liste
                    stanjaNaObradi.InsertRange(0, naObradi.mogucaSledecaStanja());
                }

                //uklanja se trenutno stanje
                stanjaNaObradi.Remove(naObradi);
            }

            return null;
        }

        //funkcija odredjuje heuristiku
        public double heuristicFunction(State s)
        {
            //trenutna heuristika je euklidsko rastojanje od ciljnog stanja
            return Math.Sqrt(Math.Pow(s.markI - Main.krajnjeStanje.markI, 2) + Math.Pow(s.markJ - Main.krajnjeStanje.markJ, 2))+s.cost;
        }

        public State getBest(List<State> stanja)
        {
            State rez = null;
            double min = Double.MaxValue;

            foreach (State s in stanja)
            {
                double h = heuristicFunction(s);
                if (h < min)
                {
                    min = h;
                    rez = s;
                }
            }
            return rez;
        }



    }
}
