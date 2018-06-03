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
            NaiveBayes textClassification = new NaiveBayes();
            DataModel train = TextUtil.LoadData("train.tsv");
             
            textClassification.fit(train);

            Console.WriteLine("Kritika: Jelena likes Harry Potter and Milos. Jelena has terrible taste in things.");
            textClassification.predict("Jelena likes Harry Potter and Milos. Jelena has terrible taste in things.");

            Console.ReadLine();
        }
    }
}
