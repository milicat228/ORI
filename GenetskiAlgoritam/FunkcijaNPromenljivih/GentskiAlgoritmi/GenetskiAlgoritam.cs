using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace GentskiAlgoritmi
{
    class GenetskiAlgoritam
    {        
        public static bool traziMAX = false;
        
        double[] MIN_X = { 1.2, -5 }; //donja
	    double[] MAX_X = { 5, -1 }; //gornja

	    int elitizam = 1;// 0 nema elitizma 1 sa elitizmom
	    int broj_jedinki = 100;
	    int broj_iteracija = 1000;

        double prag_ukrstanja = 0.3;
        double prag_mutacije  = 0.02;

        double suma_ocena = 0.0;
        double max_ocena  = 0.0;
        int index_najboljeg = 0;
        double[]  resenjeX;
    	
        Jedinka [] generacija;
	    Jedinka [] nova_generacija;

        public static double funkcija(double[] d)
        {            
            if(traziMAX == true)
                return 4 * d[0] + Math.Cos(d[1])/d[0]; //za max
            else
                return -(4 * d[0] + Math.Cos(d[1]) / d[0]); //za min
            //Ja bih za max kao na Metodama nasla od negativne verzije f-je, jer niko nije lud da menja kod            
        }

        Random rand = new Random();
        int selekcija(Jedinka[] gen)
        {
            int retVal = 0;
	        double t = suma_ocena * rand.NextDouble();
	        double s = 0.0;
	        double sp = 0.0;
           
            for(int i = 0; i < gen.Length; ++i)
            {
                s += gen[i].ocenaP;
                if(sp <= t && t <= s)
                {
                    retVal = i;
                    break;
                }

                sp = s;
            }

            return retVal;
        }

        void ukrstanje(Jedinka jedinka1, Jedinka jedinka2, double prag) 
        {
            //za svaki gen se generise slucajan broj koji odlucuje da li ce se zameniti geni u dve jedinke
	        for(int i = 0; i < Jedinka.MAXSTRING; ++i)
            {
                double slucajanBroj = rand.NextDouble();
                if( slucajanBroj < prag)
                {
                    int temp = jedinka1.chromosome[i];
                    jedinka1.chromosome[i] = jedinka2.chromosome[i];
                    jedinka2.chromosome[i] = temp;
                }
            }

            //Jedinka sadrzi i normalnu predstavu broja, mi smo menjali kodiranu predstavu, pa nju treba uskladiti
            jedinka1.x = jedinka1.bin2doubleSvePromenljive(jedinka1.chromosome);
            jedinka2.x = jedinka2.bin2doubleSvePromenljive(jedinka2.chromosome);
        }

        
        void ukrstanje2(Jedinka jedinka1, Jedinka jedinka2)
        {
            //jedinka1 treba da bude N roditelj 1 - M roditelj 2 - N roditelj 1
            //jedinka2 treba da bude N roditelj 2 - M roditelj 1 - N roditelj 2

            int N = rand.Next(0, Jedinka.MAXSTRING/2); //slucajno se generise N
            int M = Jedinka.MAXSTRING - 2 * N; 

            //menja se centralnih M hromozoma
            for(int i = N; i < N + M; ++i)
            {
                int temp = jedinka1.chromosome[i];
                jedinka1.chromosome[i] = jedinka2.chromosome[i];
                jedinka2.chromosome[i] = temp;
            }

            //Jedinka sadrzi i normalnu predstavu broja, mi smo menjali kodiranu predstavu, pa nju treba uskladiti
            jedinka1.x = jedinka1.bin2doubleSvePromenljive(jedinka1.chromosome);
            jedinka2.x = jedinka2.bin2doubleSvePromenljive(jedinka2.chromosome);

        }
   
        void mutacija(Jedinka jedinka, double prag) 
        {
	        //za svaki gen u hromozomu jednike se generise slucajni broj koji odlucuje da li se taj bit invertuje
            for(int i = 0; i < Jedinka.MAXSTRING; ++i)
            {
                double slucajanBroj = rand.NextDouble();
                if( slucajanBroj < prag)
                {
                    jedinka.chromosome[i] = 1 - jedinka.chromosome[i];
                }
            }

	        jedinka.x = jedinka.bin2doubleSvePromenljive(jedinka.chromosome);
        }        


        public double[] algoritam() 
        {
	        // prva generacija
	        generacija = new Jedinka[broj_jedinki];
	        nova_generacija = new Jedinka[broj_jedinki];

	        for (int i = 0; i < broj_jedinki; i++) 
            {
                double[] xx = new double[Jedinka.BROJ_PROMENLJIVIH];
                for(int j = 0; j < Jedinka.BROJ_PROMENLJIVIH; ++j)
                    xx[j] = rand.NextDouble() * (MAX_X[j] - MIN_X[j]) + MIN_X[j];
		        generacija[i] = new Jedinka(xx, MIN_X, MAX_X);
	         }

	        //nove generacije
	        for (int j = 0; j < broj_iteracija; j++) 
            {
                max_ocena = funkcija(generacija[0].x);
                index_najboljeg = 0;
                for (int i = 0; i < broj_jedinki; i++)
                {
                    generacija[i].ocena = funkcija(generacija[i].x);
                    if (max_ocena < generacija[i].ocena)
                    {
                        max_ocena = generacija[i].ocena;
                        index_najboljeg = i;
                    }
                }
                // odrediti ocenuP
                double minOcena = generacija[0].ocena;
                double maxOcena = generacija[0].ocena;
                for (int i = 0; i < broj_jedinki; i++)
                {
                    minOcena = Math.Min(minOcena, generacija[i].ocena);
                    maxOcena = Math.Max(maxOcena, generacija[i].ocena);
                }
                // skaliranje minOcena = 0 maxOcena = 100
                double a = 100 / (maxOcena - minOcena);
                double b = -a * minOcena;
                double ukupno = 0;
                for (int i = 0; i < broj_jedinki; i++)
                {
                    double ocenaP = (float)(a * generacija[i].ocena + b);
                    generacija[i].ocenaP = ocenaP / 100.0;
                    ukupno += generacija[i].ocenaP;
                }
                suma_ocena = ukupno;
		        resenjeX = generacija[index_najboljeg].x;
		        int start = 0;

                if(elitizam == 1) {
                    nova_generacija[0] = new Jedinka(generacija[index_najboljeg]);
                    nova_generacija[1] = new Jedinka(generacija[index_najboljeg]);
    		        start = 1;
		        }
		        for (int i = start; i < broj_jedinki / 2; i++) { // sa elitizmom
			        int roditelj1 = selekcija(generacija);
			        int roditelj2 = selekcija(generacija);

                    Jedinka A = new Jedinka(generacija[roditelj1]);
                    Jedinka B = new Jedinka(generacija[roditelj2]);
                    ukrstanje(A, B, prag_ukrstanja); 
                    mutacija(A, prag_mutacije);
                    mutacija(B, prag_mutacije);
			        nova_generacija[i * 2] = A;
			        nova_generacija[i * 2 + 1] = B;
		        }
		        for (int i = 0; i < broj_jedinki; i++) {
                    generacija[i] = nova_generacija[i];
		        }
	        }
	        return resenjeX;
        }   
    }
}
