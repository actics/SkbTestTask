using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SkbLib
{
    public class SearchEngine
    {
        /*
         * ������ ����� ��������� �������� �������� ������.
         * �� ����� �������� ������� ������� => ������ ���� ��� �����-������� 
         * ��������������� ������� ��������. ����� �������� ����� �������
         * ������� => ������ ����� <= 10 �� ����, ��������������� �������.
         * ��� �������� ������ ��� ������ ����� PrefixString
         * � ������� PrefixString ����� � ��� ��� �������� ��������� �� ���� 
         * ������ ������.
         */
        private const int AnswerSize = 10;

        private readonly Dictionary<PrefixString, PrefixString[]> _prefixDictionary;

        private SearchEngine(Dictionary<PrefixString, PrefixString[]> prefixDictionary)
        {
            _prefixDictionary = prefixDictionary;
        }

        public static SearchEngine GenerateFromDictionary(List<WordWithFreq> words)
        {
            var workDictionary = new Dictionary<PrefixString, List<PrefixWithFreq>>();

            foreach (var wordWithFreq in words)
            {
                var word = new PrefixString(wordWithFreq.Word);
                var freq = wordWithFreq.Freq;

                for (var i = 1; i <= word.Length; i++)
                {
                    var prefix = word.GetPrefix(i);

                    if (!workDictionary.ContainsKey(prefix))
                        workDictionary.Add(prefix, new List<PrefixWithFreq>());

                    workDictionary[prefix].Add(new PrefixWithFreq(word, freq));
                }
            }

            var prefixDictionary = new Dictionary<PrefixString, PrefixString[]>();

            foreach (var keyValue in workDictionary)
            {
                var array = keyValue.Value
                    .OrderByDescending(x => x.Freq)
                    .ThenBy(x => x.Prefix)
                    .Select(x => x.Prefix)
                    .Take(AnswerSize)
                    .ToArray();

                prefixDictionary.Add(keyValue.Key, array);
            }

            return new SearchEngine(prefixDictionary);
        }

        public string[] PrefixSearch(string prefix)
        {
            var prefixString = new PrefixString(prefix);

            if (!_prefixDictionary.ContainsKey(prefixString))
                return new string[0];

            return _prefixDictionary[prefixString].Select(word => word.ToString()).ToArray();
        }
    }
}
