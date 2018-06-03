using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Lavirint
{
    //TODO 7: Implementirati klasu koja modeluje senzor
    //ne mora, meni je lakse i lepse ovako
    class Senzor
    {        
        private static int radijus = 2;
        private int x;
        private int y;        

        public Senzor(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //vrati koordinate svih polja na koja deluje taj senzor
        public List<int> poljaPodSenzorom()
        {
            List<int> rez = new List<int>();

            for (int i = -radijus; i <= radijus; ++i)
            {
                int novoX = x + i;

                //proveri da li je x koordinata u lavirintu
                if (novoX < 0 || novoX >= Main.brojVrsta)
                    continue;

                for (int j = -radijus; j <= radijus; ++j)
                {
                    int novoY = y + j;

                    //proveri da li je y koordinata u lavirintu
                    if (novoY < 0 || novoY >= Main.brojKolona)
                        continue;

                    //ova koordinata je pod nadzorom senzora
                    rez.Add(novoX * 10 + novoY);
                }

            }           

            return rez;
        }
    }
}
