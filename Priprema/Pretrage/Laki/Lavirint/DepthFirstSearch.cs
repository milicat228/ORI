using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Lavirint
{
    class DepthFirstSearch
    {
        public State search(State pocetnoStanje)
        {
            //TODO 4: Implementirati pretragu prvi u dubinu
            //Slepa pretraga
            //Pretraga se koristi kada znamo da je resenje daleko od pocetnog stanja (tj. do krajnjeg stanja je portrebno preci veliki broj stanja)
            //Nije zagarantovano da daje optimalno resenje.
            List<State> stanjaNaObradi = new List<State>(); //struktura je LIFO (last in, first out) - dodavaje se na pocetak i skida sa pocetka           
            stanjaNaObradi.Add(pocetnoStanje);

            Hashtable posecenaStanja = new Hashtable(); //za pamcenja stanja (polja) u kojima je robot bio
            
            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = stanjaNaObradi[0];
                
                if(posecenaStanja.ContainsKey(naObradi.GetHashCode()) == false) //stanje se obradjuje samo ako nije obradjeno (novo je)
                {
                    Main.allSearchStates.Add(naObradi); //sluzi za prikaz u debug rezimu

                    if (naObradi.isKrajnjeStanje() == true) //provera da li je stanje krajnje
                    {
                        return naObradi;
                    }

                    //dodavanje u posecena stanja
                    posecenaStanja.Add(naObradi.GetHashCode(),null); 

                    //sva moguca sledeca stanja se dodaju na pocetak liste
                    stanjaNaObradi.InsertRange(0, naObradi.mogucaSledecaStanja());
                }

                //uklanja se trenutno stanje
                stanjaNaObradi.Remove(naObradi);
            }

            return null;
        }
    }
}
