using System;
using System.Text.RegularExpressions;
using System.Linq;

using Extensions;

namespace CalcLibrary
{

    static public class Calc
    {
        private const string _operationPattern = @"(?!-\d)[^\d,\.]";
        private static readonly Func<Match, int, string> _convertMatchToString = (match, i) => match.Value;
        private static readonly Regex regex = new Regex(_operationPattern);

#if DEBUG
        public
#endif
        static string[] GetOperands(string input)
        {
            MatchCollection collection = regex.Matches(input);

            return input.Split(collection.Select(_convertMatchToString).ToArray<string>(), StringSplitOptions.None);
        }

#if DEBUG
        public
#endif
        static string GetOperation(string input)
        {
            input = input.Replace(" ", string.Empty);
            MatchCollection collection = regex.Matches(input);

            DebugConsole.WriteLine($"Result: `{string.Join(" ", collection.Select(_convertMatchToString))}`\nLength: {collection.Count}");

            if (collection.Count > 1)
                throw new ArgumentException("Bad expression format, there is must be one operation per expression", "input");

            return collection[0].Value;
        }
        public static string DoOperation(string s)
        {
            return "";
        }
    }
}
