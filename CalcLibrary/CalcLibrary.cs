using Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CalcLibrary
{

#if DEBUG
    public
#endif
    delegate double OperationDelegate(double x, double y);
    public static class Calc
    {

#if DEBUG
        public
#endif
        static readonly Dictionary<string, OperationDelegate> DoubleOperation =
        new Dictionary<string, OperationDelegate>
        {{ "+", (x,y) => x + y},
        { "-", (x, y) => x - y },
        { "*", (x, y) => x * y},
        { "/", (x, y) => x / y }};

        private const string _operationPattern = @"(?!-\d)[^\d,\.]";
        private static readonly Func<Match, int, string> _convertMatchToString = (match, i) => match.Value;
        private static readonly Regex _operationRegex = new Regex(_operationPattern);

#if DEBUG
        public
#endif
        static string[] GetOperands(string input)
        {
            Match[] matches = GetMatches(_operationRegex, input);

            string[] result = input.Split(matches.Select(_convertMatchToString).ToArray(), StringSplitOptions.None);

            DebugConsole.WriteLine($"Operands: `{result[0]}`,`{result[1]}`");

            return result;
        }

        private static Match[] GetMatches(Regex regex, string inputString)
        {
            MatchCollection collection = regex.Matches(inputString);
            Match[] matches = new Match[collection.Count];
            for (int i = 0; i < collection.Count; i++)
            {
                matches[i] = collection[i];
            }
            collection = null;
            return matches;
        }

#if DEBUG
        public
#endif
        static string GetOperation(string input)
        {
            input = input.Replace(" ", string.Empty);
            Match[] collection = GetMatches(_operationRegex, input);

            DebugConsole.WriteLine($"Operation: `{string.Join(" ", collection.Select(_convertMatchToString))}`");

            if (collection.Length > 1)
                throw new ArgumentException("Bad expression format, there is must be one operation per expression", "input");

            return collection[0].Value;
        }
        public static string DoOperation(string s)
        {
            string[] array = GetOperands(s);
            CultureInfo real = GetCultureInfo(array);
            CultureInfo.CurrentCulture = real;
            double[] operands = Array.ConvertAll<string, double>(array, i => double.Parse(i));

            string op = GetOperation(s);

            double result = DoubleOperation[op].Invoke(operands[0], operands[1]);

            DebugConsole.WriteLine("> DoOperation start\n" +
                $"\tDecimal separator: `{real.NumberFormat.NumberDecimalSeparator}`\n" +
                $"\tOperands (string): `{array[0]}`,`{array[1]}`\n" +
                $"\tOperands: `{operands[0]}`,`{operands[1]}`\n" +
                $"\tOperator: `{op}`\n" +
                $"\tResult: {result}\n" +
                "> DoOperation end"
                );

            return $"{Math.Round(result, 3)}";
        }

        private static CultureInfo GetCultureInfo(string[] array)
        {
            Regex separatorRegex = new Regex(@"[^\d]");
            CultureInfo info = new CultureInfo(CultureInfo.CurrentCulture.Name);
            NumberFormatInfo tmp = new NumberFormatInfo();

            bool first = true;
            foreach (string i in array)
            {
                MatchCollection collection = separatorRegex.Matches(i);
                if (collection.Count > 1)
                    throw new FormatException("Invalid number format");
                else if (collection.Count == 1 && collection[0].Value.Length == 1)
                {
                    tmp.NumberDecimalSeparator = collection[0].Value;
                    if (first)
                        info.NumberFormat.NumberDecimalSeparator = tmp.NumberDecimalSeparator;
                    else if (tmp.NumberDecimalSeparator != info.NumberFormat.NumberDecimalSeparator)
                        throw new FormatException("All numbers in expression must have same decimal separator");
                    first = false;
                }
            }

            return info;
        }
    }
}
