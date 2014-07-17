using System;
using System.Collections.Generic;
using System.IO;

namespace SkbLib
{
    public static class UserInput
    {
        /*
         * Данная задача по формулировке очень напоминает стандартные ACM задачи.
         * Поэтому я решил не проверять входные данные на корректность,
         * надеясь на то, что вход всегда будет соответствовать формату,
         * данному в формулировке.
         */
        public static List<WordWithFreq> ReadDictionary(TextReader reader)
        {
            var words = new List<WordWithFreq>();

            var wordsCounterString = reader.ReadLine();
            var wordsCounter = int.Parse(wordsCounterString);

            for (var i = 0; i < wordsCounter; i++)
            {
                var line = reader.ReadLine();
                var strPiece = line.Split(null as char[], StringSplitOptions.RemoveEmptyEntries);

                var word = strPiece[0];
                var freq = int.Parse(strPiece[1]);

                words.Add(new WordWithFreq(word, freq));
            }

            return words;
        }

        public static List<string> ReadQueries(TextReader reader)
        {
            var queries = new List<string>();

            var queriesCounterString = reader.ReadLine();
            var queriesCounter = int.Parse(queriesCounterString);

            for (var i = 0; i < queriesCounter; i++)
            {
                var query = reader.ReadLine();
                queries.Add(query);
            }

            return queries;
        }
    }
}
