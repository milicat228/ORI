using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    //TODO 6: Dodati klasu koja modeluje ceo mec, tj. moguce igranje poena i cuva poene i ostalo
    class Mec
    {
        private int poeniPrvi = 0;
        private int poeniDrugi = 0;
        public String zaIspisNaKraju = "";
        private static Random rnd = new Random();

        public Mec()
        {
            poeniPrvi = 0;
            poeniDrugi = 0;
            zaIspisNaKraju = "";
        }

        public Mec(Mec m)
        {
            poeniDrugi = m.poeniDrugi;
            poeniPrvi = m.poeniPrvi;
            zaIspisNaKraju = m.zaIspisNaKraju;
        }

        public bool daLiJeZavrsenMec()
        {
            int razlika = Math.Abs(poeniDrugi - poeniPrvi);
            if (razlika >= 2 && (poeniPrvi >= 4 || poeniDrugi >= 4))
                return true;
            else
                return false;
        }

        public void plavoPolje()
        {
            ++poeniPrvi;
            zaIspisNaKraju += "Plavo + prvi igrac dobio poen. Rezultat:" + poeniPrvi.ToString() + "-" + poeniDrugi.ToString() + "\n" ;
        }

        private void celendz()
        {           
            double rand = rnd.NextDouble();
            if( rand >= 0.5)
            {
                ++poeniPrvi;
                zaIspisNaKraju += "Celendz + prvi igrac dobio poen. Rezultat:" + poeniPrvi.ToString() + "-" + poeniDrugi.ToString() + "\n";
            }
            else
            {
                ++poeniDrugi;
                zaIspisNaKraju += "Celendz + drugi igrac dobio poen. Rezultat:" + poeniPrvi.ToString() + "-" + poeniDrugi.ToString() + "\n";
            }
        }

        public void narandzasto()
        {            
            double rand = rnd.NextDouble();
            if (rand <= 0.75)
            {
                ++poeniDrugi;
                zaIspisNaKraju += "Narandzasto + drugi igrac dobio poen. Rezultat:" + poeniPrvi.ToString() + "-" + poeniDrugi.ToString() + "\n";
            }
            else
            {
                zaIspisNaKraju += "Narandzasto + igra se celendz. \n";
                celendz();
            }
        }

    }
}
