using System;
using SkbLib;

namespace Server
{
    public class QueryExecuter
    {
        private readonly SearchEngine _searchEngine;

        public QueryExecuter(SearchEngine searchEngine)
        {
            _searchEngine = searchEngine;
        }

        public string ExecuteQuery(string query)
        {
            var prefix = ExtractPrefixFromQuery(query);
            if (prefix == null)
                return null;

            var words = _searchEngine.PrefixSearch(prefix);
            return String.Join("\r\n", words);
        }

        private static string ExtractPrefixFromQuery(string query)
        {
            if (query == null)
                return null;

            var queryPiece = query.Split(null as char[], StringSplitOptions.RemoveEmptyEntries);

            if (queryPiece.Length != 2 || queryPiece[0] != "get")
                return null;

            return queryPiece[1];
        }
    }
}