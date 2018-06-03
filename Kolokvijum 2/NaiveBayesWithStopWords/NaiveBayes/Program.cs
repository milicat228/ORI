using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaiveBayes
{
    class Program
    {
        static void Main(string[] args)
        {
            TextUtil.loadStopwords();
            NaiveBayes textClassification = new NaiveBayes();
            DataModel train = TextUtil.LoadData("train.csv");
             
            textClassification.fit(train);

            DataModel test = TextUtil.LoadData("test.csv");
            int match = 0;
            foreach(String line in test.Text)
            {
                int index = test.Text.IndexOf(line);
                String job = textClassification.predict(line);
                Console.WriteLine(index + "Stvarno {0}, predvijdeno {1}", test.Sentiment[index], job);

                if (job.Equals(test.Sentiment[index]))
                    ++match;

            }

            Console.WriteLine("Pogodjeno {0} od {1}", match, test.Text.Count);

            Console.ReadLine();
        }
    }
}
