using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GentskiAlgoritmi
{
    public class Jedinka
    {
        //TODO 1: Dodati potrebne promenljive za drugu promenljivu
        public static int MAXSTRING = 28;
        public static int PRVA = 14; //do kog hromozoma je prva promenljiva
        public int MAX_INT;
        public int MAX_INT2;

        public double MIN_X;
        public double MAX_X;
        public double MIN_X2;
        public double MAX_X2;

        public double x;
        public double x2;

        public int [] chromosome;
        public double ocena;
        public double ocenaP;

        public Jedinka()
        {
            //TODO 2: Dodati potrebne inicijalizacije za drugu promenljivu
            MAX_INT = (int) Math.Pow(2, PRVA) - 1;
            MAX_INT2 = (int)Math.Pow(2, MAXSTRING - PRVA) - 1;

            x = 0;
            x2 = 0;

            chromosome = new int[MAXSTRING];
        }

        //TODO 3: Promeniti ceo konstrukor da prome min i max za obe promenljive
        public Jedinka(double aMIN_X, double aMAX_X, double bMIN_X, double bMAX_X) 
        {
            MAX_INT = (int)Math.Pow(2, PRVA) - 1;
            MAX_INT2 = (int)Math.Pow(2, MAXSTRING - PRVA) - 1;

            MIN_X = aMIN_X;
	        MAX_X = aMAX_X;
            MIN_X2 = bMIN_X;
            MAX_X2 = bMAX_X;

	        x = MIN_X;
            x2 = MIN_X2;

	        chromosome = int2bin((int)x,(int)x2); //ova funkcija je isto menjana
        }
        
        //TODO 4: Promeniti i ovaj konstruktor... Majko mila
        public Jedinka(double newX, double aMIN_X, double aMAX_X, double newX2, double bMIN_X, double bMAX_X) 
        {
            MAX_INT = (int)Math.Pow(2, PRVA) - 1;
            MAX_INT2 = (int)Math.Pow(2, MAXSTRING - PRVA) - 1;

            MIN_X = aMIN_X;
            MAX_X = aMAX_X;
            MIN_X2 = bMIN_X;
            MAX_X2 = bMAX_X;

            x = newX;
            x2 = newX2;

            chromosome = int2bin(convert_double2int(x), convert_double2int2(x2));
        }

        //TODO 5: Mrzim konstruktore
        public Jedinka(Jedinka jedinka)
        {
            MAX_INT = (int)Math.Pow(2, PRVA) - 1;
            MAX_INT2 = (int)Math.Pow(2, MAXSTRING - PRVA) - 1;

            MIN_X = jedinka.MIN_X;
	        MAX_X = jedinka.MAX_X;
            MIN_X2 = jedinka.MIN_X2;
            MAX_X2 = jedinka.MAX_X2;

	        x = jedinka.x;
            x2 = jedinka.x2;

	        chromosome = int2bin((int)x, (int)x2);
        }

        //TODO 6: Promeniti nacin kodiranja. Donjih PRVA bita je prva promenljiva, ostali su druga promenljiva
        int [] int2bin(int b, int a)
        {
            int[] retVal = new int[MAXSTRING];
            for (int i = 0; i < PRVA; i++)
            {
                int o = (int)b % 2;
                b = b / 2;
                retVal[i] = o;
            }
            for (int i = PRVA; i <MAXSTRING; ++i)
            {
                int o = (int)a % 2;
                a = a / 2;
                retVal[i] = o;
            }

            return retVal;
        }
        
        public int convert_double2int(double aX)
        {
	        return (int)((aX-MIN_X)*MAX_INT/(MAX_X-MIN_X));
        }

        double convert_int2double(int a)
        {
	        return a*(MAX_X-MIN_X)/MAX_INT+MIN_X;
        }

        //TODO 7: Bukvalno kopirati dve metode iznad da rade isto, ali sa drugim konstantama, jer...
        public int convert_double2int2(double aX)
        {
            return (int)((aX - MIN_X2) * MAX_INT2 / (MAX_X2 - MIN_X2));
        }

        double convert_int2double2(int a)
        {
            return a * (MAX_X2 - MIN_X2) / MAX_INT2 + MIN_X2;
        }

        //TODO 8: I ovu metodu kopirati sa drugim konstantama
        public double bin2double(int [] bin) {
            int ret = 0;
            int b = 1;
            for (int i = 0; i < PRVA; i++) //PAZNJA!
            {
                ret += bin[i] * b;
                b = b * 2;
            }
            return convert_int2double(ret);
        }

        public double bin2double2(int[] bin)
        {
            int ret = 0;
            int b = 1;
            for (int i = PRVA; i < MAXSTRING; i++) //PAZNJA!
            {
                ret += bin[i] * b;
                b = b * 2;
            }
            return convert_int2double2(ret); //PAZNJA!
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
