using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaiveBayes
{
    public class NaiveBayes
    {
        //documents_sentiment_count pamti broj dokumenata(recenica) za svaki od sentimenata
        //0 - negativno, 1 - pozitivno
        Dictionary<int, double> documents_sentiment_count = new Dictionary<int, double>();
        //vocabulary pamti za svaku rec koliko puta se pojavila, bez obzira u kom kontekstu (pozitivno ili negativno)
        public static Dictionary<string, int> vocabulary = new Dictionary<string, int>();
        //word_counts, za svaki sentiment cuva recnik koji broj koliko puta se pojavila koja rec(u tom sentimentu)
        public static Dictionary<int, Dictionary<string, int>> word_counts = new Dictionary<int, Dictionary<string, int>>();

        public NaiveBayes()
        {
            documents_sentiment_count[0] = 0.0;
            documents_sentiment_count[1] = 0.0;
            word_counts[0] = new Dictionary<string, int>();
            word_counts[1] = new Dictionary<string, int>();
        }
        /// <summary>
        /// Formiranje globalnog recnika, recnika iz pozitivnih i negativnih recenzija 
        /// </summary>
        /// <param name="model">train model ucitan iz tsv datoteke</param>
        public void fit(DataModel model)
        {
            for (int i = 0; i < model.Text.Count; i++)
            {
                string text = model.Text[i];
                int sentiment = model.Sentiment[i];

                documents_sentiment_count[sentiment] += 1;
                string[] words = TextUtil.Tokenize(text);
                //ovo je pomocni recnik koji zna koliko puta se rec pojavila u recenici
                Dictionary<string, int> counts = TextUtil.CountWords(words);                

                foreach (KeyValuePair<string, int> item in counts)
                {
                    string word = item.Key;
                    int count = item.Value;

                    //TODO 5 - Popuniti globalni recnik svih reci, kao i recnike za odredjene sentimente
                    //globalni recnik - cuva broj pojava svake reci u recniku
                    if (!vocabulary.ContainsKey(word))
                    {
                        vocabulary.Add(word, count);
                    }
                    else
                    {
                        vocabulary[word] += count;
                    }

                    //recnik sentimena cuva koliko puta se rec pojavila u tom sentimentu
                    if (!word_counts[sentiment].ContainsKey(word))
                    {
                        word_counts[sentiment].Add(word, count);
                    }
                    else
                    {
                        word_counts[sentiment][word] += count;
                    }
                }
            }
        }
        /// <summary>
        /// Racunanje verovatnoca za prosledjeni tekst
        /// </summary>
        /// <param name="text">Tekst koji se klasifikuje</param>
        public void predict(string text)
        {
            string[] words = TextUtil.Tokenize(text);
            //CountWords vraca mapu rec, brojPojava u recenici
            var counts = TextUtil.CountWords(words);

            double documentCount = documents_sentiment_count.Values.Sum();
            //TODO 6 - Izracunati verovatnoce da je dokument za predikciju bas pozitivnog ili negativnog sentimenta - P(cj)
            double Pcj_neg = documents_sentiment_count[0] / documentCount; //verovatnoca negativanog sentimenta
            double Pcj_pos = documents_sentiment_count[1] / documentCount; //verovatnoca pozitivnog sentimenta

            double log_prob_neg = 0.0; //verovatnoca pojave reci, ako je sentiment negativan (svih reci)
            double log_prob_pos = 0.0; //verovatnoca pojave reci, ako je sentiment pozitivan (svih reci)
            //prolazimo kroz sve reci iz prosledjene recenice
            //trazimo koja je verovatnoca da se data rec pojavila, ako je sentiment negativan, a koja ako je pozitivan            
            foreach (KeyValuePair<string, int> item in counts)
            {
                string w = item.Key;
                int cnt = item.Value;
                /*if (w.Length <= 3)
                    continue;*/
                //TODO 7.1 - Iterativno racunati logaritamski zbir verovatnoca sentimenta svake reci
                double wordCount = vocabulary.Count;
                //racuna se P(xj|cj), gde je xj trenutna rec
                double neg = 0.0; //verovatnoca za samo jednu (trenutnu rec)
                double pos = 0.0; //verovatnoca za samo jednu (trenutnu rec)

                // +1 i wordCount se dodaju zbog reci koje se nigde ne spominju (po Laplasovom poravnanju)
                // tako se izbegava da je verovatnoca reci jednaka 0
                // word_counts[0].Values.Sum() ukupan broj reci u dokumentu nekog sentimena
                // if je dodan jer bi recnik bacio null ako ne postoji kljuc
                if ( word_counts[0].ContainsKey(w)) //da li se rec spominje u negativnim tekstovima
                    neg = (word_counts[0][w] + 1) / (word_counts[0].Values.Sum() + wordCount);
                else
                    neg =  1 / (word_counts[0].Values.Sum() + wordCount);

                if (word_counts[1].ContainsKey(w)) //da li se rec spominje u pozitivnim tekstovima
                    pos = (word_counts[1][w] + 1) / (word_counts[1].Values.Sum() + wordCount);
                else
                    pos = 1 / (word_counts[1].Values.Sum() + wordCount);

                //cnt - koliko se puta trenutna rec pojavila u tekstu
                log_prob_neg += cnt * Math.Log(neg);
                log_prob_pos += cnt * Math.Log(pos);
            }

            //TODO 7.2 Izracunati konacnu vrednost verovatnoce sentimenta prosledjenog teksta
            //ovo je onda zadnja formula sa sumom logaritama 
            //logaritmi se koriste da bi se izbegao underflow (zbog sabiranja verovatnoca koje su brojevi izmedju 0 i 1
            log_prob_neg = log_prob_neg + Math.Log(Pcj_neg);
            log_prob_pos = log_prob_pos + Math.Log(Pcj_pos);

            //TODO 8 - Ispisati vrednosti predikcije za pozitivan i negativan sentiment teksta
            //logaritam je rastuca funkcija
            //dakle za manju verovatnocu ce biti manji (vise negativan broj)
            //tekst je onog sentimena cija verovatnoca je veca
            Console.WriteLine("neg: {0}", log_prob_neg);
            Console.WriteLine("pos: {0}", log_prob_pos);

            if(log_prob_pos == log_prob_neg)
            {
                Console.WriteLine("Nije moguce odrediti.");
            }
            else if(log_prob_pos > log_prob_neg)
            {
                Console.WriteLine("Pozitivna kritika.");
            }
            else
            {
                Console.WriteLine("Negativna kritika.");
            }
        }
    }
}
