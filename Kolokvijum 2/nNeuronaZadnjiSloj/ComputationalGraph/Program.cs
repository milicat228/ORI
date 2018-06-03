using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputationalGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            Utils u = new Utils();
            u.readFile();
           
            NeuralNetwork network = new NeuralNetwork();
            network.Add(new NeuralLayer(4, 4, "sigmoid"));
            //Zadnji sloj ima 3 neurona. Svaki neuron predvidja verovatnocu za jedan tip.
            //Zbog toga u utils rasporedjeni ulazi kao 1, 0, 0 ako je tip_1. To npr. kaze prvom neuronu da on treba da napravi
            //svoj izlaz da bude 1, dok ostala dva neurona prave svoje izlaze na 0
            //da bi mreza radila sa 3 neurona u zadnjem sloju potrebno je menjati NeuralNetwork klasu
            network.Add(new NeuralLayer(4, 3, "sigmoid"));
            Console.WriteLine("Obuka pocela.");
            network.fit(u.train, u.trainY, 0.1, 0.9, 1500);
            Console.WriteLine("Kraj obuke.");

            int match = 0;
            foreach (List<double> input in u.test)
            {
                int i = u.test.IndexOf(input);                
                List<double> prediction = network.predict(input);
                int realTip = Array.IndexOf(u.testY[i].ToArray(), u.testY[i].Max());
                int tip= Array.IndexOf(prediction.ToArray(), prediction.Max());
                Console.WriteLine("Stvarni tip {0}, predvidjeni tip {1}", realTip + 1, tip + 1);
                if( realTip == tip)
                {
                    ++match;
                }
            }

            Console.WriteLine("Pogodjeno {0} od {1}", match, u.test.Count);
            Console.WriteLine("Tacnost: {0} %", match * 100 / u.test.Count);
            
            Console.ReadLine();
           
        }

        


    }
}
