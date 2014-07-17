using System;
using System.Collections.Generic;
using System.IO;
using SkbLib;

namespace Server
{
    class Program
    {
        private static Tuple<string, int> ParseCommandLine(string[] args)
        {
            if (args.Length != 2)
                throw new ApplicationException(
                    "Серверу передано неправильное количество аргументов. Необходимо: путь к текстовому файлу и порт");

            if (!File.Exists(args[0]))
                throw new ApplicationException("Файл не существует");

            UInt16 port;
            if (!UInt16.TryParse(args[1], out port))
                throw new ApplicationException("Невалидный номер порта");

            return new Tuple<string, int>(args[0], port);
        }

        private static void Main(string[] args)
        {
            var arguments = ParseCommandLine(args);

            var filename = arguments.Item1;
            var port = arguments.Item2;

            List<WordWithFreq> dictionary;
            using (var reader = File.OpenText(filename))
                dictionary = UserInput.ReadDictionary(reader);

            var searchEngine = SearchEngine.GenerateFromDictionary(dictionary);
            var queryExecuter = new QueryExecuter(searchEngine);
            var server = new AutocompliteServer(port, queryExecuter);

            server.Forever();
        }
    }
}