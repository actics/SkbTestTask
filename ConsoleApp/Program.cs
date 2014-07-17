using System;
using System.Linq;
using SkbLib;

namespace ConsoleApp
{
    class Program
    {
        private static void Main()
        {
            var dictionary = UserInput.ReadDictionary(Console.In);
            var queries = UserInput.ReadQueries(Console.In);
            var searchEngine = SearchEngine.GenerateFromDictionary(dictionary);

            var answers = queries.Select(searchEngine.PrefixSearch);
            foreach (var answer in answers)
            {
                Console.WriteLine(string.Join(Environment.NewLine, answer));
                Console.WriteLine();
            }
        }
    }
}
