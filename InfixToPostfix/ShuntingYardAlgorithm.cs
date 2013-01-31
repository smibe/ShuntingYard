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
        public Stack<string> _previousTokens;
        public string _previous;
        protected string _result;
        
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

            AppendAllPreviousTokens();
        }

        private void AppendAllPreviousTokens()
        {
            while (_previousTokens.Count > 0)
                AppendPreviousToken();
        }

        private void HandleOperator(string current)
        {
            while (!ExecutesBeforePrevious(current))
                AppendPreviousToken();
            _previousTokens.Push(current);
        }

        private bool ExecutesBeforePrevious(string current)
        {
            if (_previousTokens.Count <= 0)
                return true;

            return GetPrecedence(current) > GetPrecedence(_previousTokens.Peek());
        }

        private int GetPrecedence(string p)
        {
            switch (p)
            {
                case "*":
                    return 100;
                default:
                    return 10;
            }
        }

        private void AppendPreviousToken()
        {
            if (_previousTokens.Count > 0)
                AppendToken(_previousTokens.Pop());
        }

        private void Initialize(string expression)
        {
            _tokens = (expression ?? String.Empty).Split(' ');
            _result = string.Empty;
            _previous = string.Empty;
            _previousTokens = new Stack<string>();
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

    }
}
