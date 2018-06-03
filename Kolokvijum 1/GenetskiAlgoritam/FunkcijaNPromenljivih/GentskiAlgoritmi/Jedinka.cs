using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GentskiAlgoritmi
{
    public class Jedinka
    {
       
        public static int MAXSTRING = 28; //duzina niza koji se koristi za predstavu svih promenljvih
        public static int BROJ_PROMENLJIVIH = 2; 
        public int MAX_INT;

        public double[] MIN_X;
        public double[] MAX_X;

        public double[] x;
        public int [] chromosome;

        public double ocena;
        public double ocenaP;

        public Jedinka()
        {
            int jedna = MAXSTRING / BROJ_PROMENLJIVIH;
            MAX_INT = (int) Math.Pow(2, jedna) - 1;

            x = new double[BROJ_PROMENLJIVIH];
            //vrednosti svih promenljivih se postavljaju na 0            
            for(int i = 0; i < BROJ_PROMENLJIVIH; ++i)
            {
                x[i] = 0;
            }

            chromosome = new int[MAXSTRING];
        }

        public Jedinka(double[] aMIN_X, double[] aMAX_X) 
        {
            int jedna = MAXSTRING / BROJ_PROMENLJIVIH;
            MAX_INT = (int)Math.Pow(2, jedna) - 1;

            MIN_X = new double[BROJ_PROMENLJIVIH]; 
	        MAX_X = new double[BROJ_PROMENLJIVIH]; 
	        x = new double[BROJ_PROMENLJIVIH]; 

            for(int i = 0; i < BROJ_PROMENLJIVIH; ++i)
            {
                MIN_X[i] = aMIN_X[i];
                MAX_X[i] = aMAX_X[i];
                x[i] = aMIN_X[i];
            }

            chromosome = int2binSvePromenljive(x);
        }

        public Jedinka(double[] newX, double[] aMIN_X, double[] aMAX_X) 
        {
            int jedna = MAXSTRING / BROJ_PROMENLJIVIH;
            MAX_INT = (int)Math.Pow(2, jedna) - 1;

            MIN_X = new double[BROJ_PROMENLJIVIH];
            MAX_X = new double[BROJ_PROMENLJIVIH];
            x = new double[BROJ_PROMENLJIVIH];

            for (int i = 0; i < BROJ_PROMENLJIVIH; ++i)
            {
                MIN_X[i] = aMIN_X[i];
                MAX_X[i] = aMAX_X[i];
                x[i] = newX[i];
            }

            chromosome = int2binSvePromenljive(x);
        }

        public Jedinka(Jedinka jedinka)
        {
            int jedna = MAXSTRING / BROJ_PROMENLJIVIH;
            MAX_INT = (int)Math.Pow(2, jedna) - 1;

            MIN_X = new double[BROJ_PROMENLJIVIH];
            MAX_X = new double[BROJ_PROMENLJIVIH];
            x = new double[BROJ_PROMENLJIVIH];

            for (int i = 0; i < BROJ_PROMENLJIVIH; ++i)
            {
                MIN_X[i] = jedinka.MIN_X[i];
                MAX_X[i] = jedinka.MAX_X[i];
                x[i] = jedinka.x[i];
            }
            chromosome = int2binSvePromenljive(x);
        }

        int[] int2binSvePromenljive(double[] x)
        {
            int[] retVal = new int[MAXSTRING];
            int jedna = MAXSTRING / BROJ_PROMENLJIVIH;

            for (int i = 0; i < BROJ_PROMENLJIVIH; ++i)
            {
                int[] promenljiva_i = int2bin( (int)x[i] );
                for(int j = 0; j < promenljiva_i.Length; ++j)
                {
                    retVal[j + i * jedna] = promenljiva_i[j];
                }
            }

            return retVal;
        }

        int [] int2bin(int b)
        {
            int jedna = MAXSTRING / BROJ_PROMENLJIVIH;
            int[] retVal = new int[jedna];            
            for (int i = 0; i < jedna; i++)
            {
                int o = (int)b % 2;
                b = b / 2;
                retVal[i] = o;
            }
            return retVal;
        }
        
        public int convert_double2int(double aX, int indeks)
        {
	        return (int)((aX-MIN_X[indeks])*MAX_INT/(MAX_X[indeks] - MIN_X[indeks]));
        }

        double convert_int2double(int a, int indeks)
        {
	        return a*(MAX_X[indeks] - MIN_X[indeks]) /MAX_INT+MIN_X[indeks];
        }

        public double[] bin2doubleSvePromenljive(int[] bin)
        {
            double[] retVal = new double[BROJ_PROMENLJIVIH];
            int jedna = MAXSTRING / BROJ_PROMENLJIVIH;

            for (int i = 0; i <BROJ_PROMENLJIVIH; ++i)
            {
                int[] nizSamoJedneProm = new int[jedna];
                for(int j = 0; j < jedna; ++j)
                {
                    nizSamoJedneProm[j] = bin[j + i * jedna];
                }

                retVal[i] = bin2double(nizSamoJedneProm, i);
            }

            return retVal;
        }

        public double bin2double(int [] bin, int indeks) {
            int ret = 0;
            int b = 1;
            int jedna = MAXSTRING / BROJ_PROMENLJIVIH;
            for (int i = 0; i < jedna; i++)
            {
                ret += bin[i] * b;
                b = b * 2;
            }
            return convert_int2double(ret, indeks);
        }

        public override String ToString()
        {
            String retVal = "";
            for (int i = 0; i < MAXSTRING - 1; i++)
            {
                retVal += chromosome[i];
            }
            return retVal;
        }
    }
}
