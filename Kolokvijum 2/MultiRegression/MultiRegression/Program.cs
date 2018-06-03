using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRegression
{
    class Program
    {
        static void Main(string[] args)
        {
            Utils train = new Utils();
            train.loadData("train.csv");

            MultiRegression mr = new MultiRegression();
            mr.Train(train.X, 20000, 0.2);

            int match = 0;
            for(int i = 0; i < train.X.Length; ++i)
            {
                double output = mr.ComputeOutput(train.X[i]);
                Console.WriteLine("Stvarno {0}, Predvidjeno {1}", train.X[i][3], output);

                if (output > 0.5)
                    output = 1;
                else
                    output = 0;

                if (output == train.X[i][3])
                    ++match;
            }

            Console.WriteLine("Trening set: Pogodjeno {0} od {1}. Tacnost {2} %", match, train.X.Length, match*100/train.X.Length);

            Utils test = new Utils();
            test.loadData("test.csv");

            match = 0;
            for (int i = 0; i < test.X.Length; ++i)
            {
                double output = mr.ComputeOutput(test.X[i]);
                Console.WriteLine("Stvarno {0}, Predvidjeno {1}", test.X[i][3], output);

                if (output > 0.5)
                    output = 1;
                else
                    output = 0;

                if (output == test.X[i][3])
                    ++match;
            }

            Console.WriteLine("Test set: Pogodjeno {0} od {1}. Tacnost {2} %", match, test.X.Length, match * 100 / test.X.Length);

            Console.ReadLine();
        }
    }
}
