using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InfixToPostfix
{
    class ShuntingYardAlgorithm
    {
        private string[] _tokens;
        public string Transform(string expression)
        {
            Initialize(expression);

            ProcessTokens();

            return _result;
        }

        private void ProcessTokens()
        {
            foreach (string current in _tokens)
            {
                if (IsLiteral(current))
                {
                    AppendToken(current);
                }
                else
                {
                    HandleOperator(current);
                }
            }

            AppendToken(_previous);
        }

        private void HandleOperator(string current)
        {
            AppendToken(_previous);
            _previous = current;

        }

        private void Initialize(string expression)
        {
            _tokens = (expression ?? String.Empty).Split(' ');
            _result = string.Empty;
            _previous = string.Empty;
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
        public string _previous { get; set; }
    }
}
