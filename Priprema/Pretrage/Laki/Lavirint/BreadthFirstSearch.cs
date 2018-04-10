using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Lavirint
{
    class BreadthFirstSearch
    {
        public State search(State pocetnoStanje)
        {
            //TODO 5: Implementirati pretragu prvi u sirinu
            //Slepa pretraga          
            //Daje optimalno resenje.
            List<State> stanjaNaObradi = new List<State>(); //struktura je FIFO (first in, first out) - skida se sa pocetka, dodaje na kraj           
            stanjaNaObradi.Add(pocetnoStanje);

            Hashtable posecenaStanja = new Hashtable(); //za pamcenja stanja (polja) u kojima je robot bio

            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = stanjaNaObradi[0];

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
                    stanjaNaObradi.InsertRange(stanjaNaObradi.Count, naObradi.mogucaSledecaStanja());
                }

                //uklanja se trenutno stanje
                stanjaNaObradi.Remove(naObradi);
            }

            return null;
        }
    }
}
