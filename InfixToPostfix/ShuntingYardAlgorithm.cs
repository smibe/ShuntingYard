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
            _result = string.Empty;
            string previous = string.Empty;

            foreach(string current in tokens)
            {
                if (IsLiteral(current))
                    AppendToken(current);
                else
                {
                    AppendToken(previous);
                    previous = current;
                }
            }

            AppendToken(previous);

            return _result;
        }

        private bool IsLiteral(string token)
        {
            return Regex.IsMatch(token, @"(\w+|\d+)");
        }

        void AppendToken(string token)
        {
            if (token == "")
                return;
            
            if (_result.Length > 0)
                _result += ' ';

            _result += token;
        }
        public string _result { get; set; }
    }
}
