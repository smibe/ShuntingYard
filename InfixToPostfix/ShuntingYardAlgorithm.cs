using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InfixToPostfix
{
    class ShuntingYardAlgorithm
    {
        public string Transform(string expression)
        {
            string[] tokens = (expression ?? String.Empty).Split(' ');
            string result = string.Empty;
            string previous = string.Empty;

            foreach(string current in tokens)
            {
                if (IsLiteral(current))
                    result += current;
                else
                {
                    if (previous != "")
                        result += previous;
                    previous = current;
                }
            }

            result += previous;

            return result;
        }

        private bool IsLiteral(string current)
        {
            return Regex.IsMatch(current, @"(\w+|\d+)");
        }
    }
}
