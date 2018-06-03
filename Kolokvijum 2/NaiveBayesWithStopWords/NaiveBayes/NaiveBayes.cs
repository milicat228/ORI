using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaiveBayes
{
    public class NaiveBayes
    {
        //documents_sentiment_count pamti broj dokumenata(recenica) za svaki od sentimenata
        //za svaki posao pamti koliko puta ga je dobio opisanog
        Dictionary<String, double> documents_sentiment_count = new Dictionary<String, double>();
        //vocabulary pamti za svaku rec koliko puta se pojavila, bez obzira u opisu kog posla
        public static Dictionary<string, int> vocabulary = new Dictionary<string, int>();
        //za svaki posao pamti koliko puta se u opisima pojavila ta rec
        public static Dictionary<String, Dictionary<string, int>> word_counts = new Dictionary<String, Dictionary<string, int>>();

        public NaiveBayes()
        {            
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
                string sentiment = model.Sentiment[i];

                //dodati novi sentiment u brojac review-a sa tom ocenom, ako do sada nije bila ta ocena, inace 
                //povecati broj dokumenata
                if (documents_sentiment_count.ContainsKey(sentiment))
                    documents_sentiment_count[sentiment] += 1;
                else
                {
                    //ako se ocena prvi put pojavljuje dodati recnik za nju
                    word_counts.Add(sentiment, new Dictionary<string, int>());
                    documents_sentiment_count.Add(sentiment, 1);
                }
                    

                string[] words = TextUtil.Tokenize(text);

                //ovo je pomocni recnik koji zna koliko puta se rec pojavila u recenici
                Dictionary<string, int> counts = TextUtil.CountWords(words);                

                foreach (KeyValuePair<string, int> item in counts)
                {
                    string word = item.Key;
                    int count = item.Value;
                   
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
        public String predict(string text)
        {
            string[] words = TextUtil.Tokenize(text);
            //CountWords vraca mapu rec, brojPojava u recenici
            var counts = TextUtil.CountWords(words);

            double documentCount = documents_sentiment_count.Values.Sum();            

            //racunaju se verovatnoce svake ocene
            List<double> Pcj = new List<double>();
            string[] sentiments = word_counts.Keys.ToArray();
            for (int i = 0; i < sentiments.Length; ++i)
            {
                Pcj.Add(documents_sentiment_count[sentiments[i]] / documentCount);
            }

            //verovatnoca pojave svih reci iz teksta, ako je ocena neka
            List<double> log_prob = new List<double>();
            for (int i = 0; i < sentiments.Length; ++i)
            {
                log_prob.Add(0.0);
            }

            //prolazimo kroz sve reci iz prosledjene recenice
            //trazimo koja je verovatnoca da se data rec pojavila, ako je sentiment negativan, a koja ako je pozitivan            
            foreach (KeyValuePair<string, int> item in counts)
            {
                string w = item.Key;
                int cnt = item.Value;

                if (w.Length < 2)
                    continue;
                
                double wordCount = vocabulary.Count;
                //racuna se P(xj|cj), gde je xj trenutna rec
                List<double> wordProb = new List<double>();

                for (int i = 0; i < sentiments.Length; ++i)
                {
                    if (word_counts[sentiments[i]].ContainsKey(w)) //da li se rec spominje u negativnim tekstovima
                        wordProb.Add((word_counts[sentiments[i]][w] + 1) / (word_counts[sentiments[i]].Values.Sum() + wordCount));
                    else
                        wordProb.Add((word_counts[sentiments[i]].Values.Sum() + wordCount));
                }

                //dodaj ovu rec na ukupno
                for (int i = 0; i < word_counts.Keys.Count; ++i)
                {
                    log_prob[i] += cnt * Math.Log(wordProb[i]);
                }               
            }

            for (int i = 0; i < word_counts.Keys.Count; ++i)
            {
                log_prob[i] += log_prob[i] + Math.Log(Pcj[i]);
            }

            var maxIndex = log_prob.IndexOf(log_prob.Min());

            return sentiments[maxIndex];            
        }
    }
}
