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
            network.Add(new NeuralLayer(4, 10, "sigmoid"));
            network.Add(new NeuralLayer(10, 18, "sigmoid"));
            network.Add(new NeuralLayer(18, 16, "sigmoid"));
            Console.WriteLine("Obuka pocela.");
            network.fit(u.train, u.trainY, 0.1, 0.9, 300);
            Console.WriteLine("Kraj obuke.");

            Console.WriteLine("Train podaci");
            int match = 0;
            foreach (List<double> input in u.train)
            {
                int i = u.train.IndexOf(input);
                List<double> prediction = network.predict(input);
                int realTip = Array.IndexOf(u.trainY[i].ToArray(), u.trainY[i].Max());
                int tip = Array.IndexOf(prediction.ToArray(), prediction.Max());
                //Console.WriteLine("Stvarni tip {0}, predvidjeni tip {1}", u.MSSubClassValue[realTip], u.MSSubClassValue[tip]);
                if (realTip == tip)
                {
                    ++match;
                }
            }

            Console.WriteLine("Pogodjeno {0} od {1}", match, u.train.Count);
            Console.WriteLine("Tacnost na train podacima: {0} %", match * 100 / u.train.Count); 

            Console.WriteLine("Test podaci");
            match = 0;
            foreach (List<double> input in u.test)
            {
                int i = u.test.IndexOf(input);                
                List<double> prediction = network.predict(input);
                int realTip = Array.IndexOf(u.testY[i].ToArray(), u.testY[i].Max());
                int tip= Array.IndexOf(prediction.ToArray(), prediction.Max());
                Console.WriteLine("Stvarni tip {0}, predvidjeni tip {1}", u.MSSubClassValue[realTip], u.MSSubClassValue[tip]);
                if( realTip == tip)
                {
                    ++match;
                }
            }

            Console.WriteLine("Pogodjeno {0} od {1}", match, u.test.Count);
            Console.WriteLine("Tacnost na test podacima: {0} %", match * 100 / u.test.Count);
            
            Console.ReadLine();
           
        }

        


    }
}
