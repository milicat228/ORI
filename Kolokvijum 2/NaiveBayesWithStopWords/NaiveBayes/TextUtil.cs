using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace NaiveBayes
{
    public static class TextUtil
    {

        public static String[] stopwords;

        public static void loadStopwords()
        {
            string fileName = string.Format(@"./data/english_stopwords.txt");
            stopwords = File.ReadAllLines(fileName);
        }



        /// <summary>
        /// Ucitava skup podataka i popunjava model podataka. 
        /// </summary>
        /// <param name="name">Naziv fajla u data direktorijumu</param>
        /// <returns>Model podataka sa popunjenim podacima</returns>
        public static DataModel LoadData(string name)
        {
            DataModel dataModel = new DataModel();
            string fileName = string.Format(@"./data/{0}", name);
            string[] lines = File.ReadAllLines(fileName);          

            for (int i = 0; i < lines.Length; i++)            {                string[] parts = lines[i].Split(',');

                //dataModel.Text.Add(parts[1]);
                dataModel.Text.Add(clearTextFromStopWord(parts[5]));                                dataModel.Sentiment.Add(parts[0]);            }            
            return dataModel;
        }   
        
        private static String clearTextFromStopWord(String text)
        {
            foreach(String stopword in stopwords)
            {
                text = text.Replace(stopword, " ");
            }
            return text;
        }

        private static int findTitle(String title, String[] titles)
        {
            for (int i = 0; i < titles.Length; ++i)
            {
                if (title.Equals(titles[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Metoda za uklanjanje znakova interpunkcije iz teksta.
        /// Npr. "Milan,Darko i Ivan polazu kolokvijum!" ce biti transformisan u
        /// "Milan Darko i Ivan polazu kolokvijum"
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Tekst bez znakova interpunkcije</returns>
        public static string RemovePunctuation(string text)
        {              
            string retVal = null;
            retVal = new string(text.ToCharArray().Where(c => !char.IsPunctuation(c)).ToArray());
            return retVal;
        }

        /// <summary>
        /// Tokenizacija teksta na reci. Pre razdvajanja teksta na reci (tokene),
        /// potrebno je ukloniti sve znake interpunkcije i pretvoriti 
        /// sva slova u mala. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Niz tokena (reci)</returns>
        public static string[] Tokenize(string text)
        {              
            text = RemovePunctuation(text);
            text = text.ToLower();
            string[] tokens = null;
            //ovako se uklanjaju svi razmaci
            //mada mislim da je Stefan rekao je da je dovoljno da se gleda kao da je uvek jedan
            //alternativno:             
            //tokens = Regex.Split(text, @"\s+").Where(s => s != string.Empty).ToArray();
            tokens = text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            return tokens;
        }
        /// <summary>
        /// Metoda za brojanje reci u teksu. Formira se recnik ciji je kljuc
        /// sama rec, a vrednost broj pojavljivanja te reci.
        /// Npr za niz reci: ["rec1", "rec2", "rec1"] bice formiran recnik
        ///     { "rec1" : 2,
        ///       "rec2" : 1   
        ///     } 
        /// </summary>
        /// <param name="words">Niz reci</param>
        /// <returns></returns>
        public static Dictionary<string, int> CountWords(string[] words)
        {               
            Dictionary<string, int> vocabulary = new Dictionary<string, int>();
            foreach(String word in words)
            {
                if( vocabulary.ContainsKey(word))
                {
                    vocabulary[word] ++;
                }
                else
                {
                    vocabulary.Add(word, 1);
                }
            }
            return vocabulary;
        }
    }
}
