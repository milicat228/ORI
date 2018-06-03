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
            network.Add(new NeuralLayer(4, 3, "sigmoid"));
            network.Add(new NeuralLayer(3, 2, "sigmoid"));
            network.Add(new NeuralLayer(2, 1, "sigmoid"));
            Console.WriteLine("Obuka pocela.");
            network.fit(u.train, u.trainY, 0.1, 0.9, 1500);
            Console.WriteLine("Kraj obuke.");

            int match = 0;
            foreach (List<double> input in u.train)
            {
                int i = u.train.IndexOf(input);                
                List<double> prediction = network.predict(input);
                Console.WriteLine("Stvarno {0}, predvidjeno: {1}", toTip(u.trainY[i][0]), toTip(prediction[0]));
                if (toTip(u.trainY[i][0]).Equals(toTip(prediction[0])))
                {
                    ++match;
                }
            }
            Console.WriteLine("Pogodjeno {0} od {1}", match, u.train.Count);
            Console.WriteLine("Tacnost: {0} %", match * 100 / u.train.Count);

            Console.ReadLine();
        }

        private static String toTip(double value)
        {
            // 0  je tip_1, 0.5 je tip_2, 1 je tip_3
            //pronalazi sta je najblize i kaze da je to tip
            double[] distance = { value, Math.Abs(0.5 - value), Math.Abs(1 - value) };
            int minIndex = Array.IndexOf(distance, distance.Min());
            return "tip_" + (minIndex + 1);
        }


    }
}
