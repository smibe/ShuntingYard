using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InfixToPostfix
{
    class ShuntingYardAlgorithm
    {
        private List<string> _tokens = new List<string>();
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
                    HandleLiteral(current);
                }
                else if (IsOpenParanthesis(current))
                {
                    HandleOpenParanthesis();
                }
                else if (IsClosedParanthesis(current))
                {
                    HandleClosedParanthesis();
                }
                else
                {
                    HandleOperator(current);
                }
            }

            AppendAllPreviousTokens();
        }

        private void HandleClosedParanthesis()
        {

            while (this._previousTokens.Count > 0 && this._previousTokens.Peek() != "(")
                AppendPreviousToken();
            
            if (_previousTokens.Count > 0)
		    _previousTokens.Pop();
        }

        private bool IsClosedParanthesis(string token)
        {
            return ")".Equals(token);
        }

        private void HandleOpenParanthesis()
        {
            _previousTokens.Push("(");
        }

        private bool IsOpenParanthesis(string token)
        {
            return "(".Equals(token);
        }

        private void HandleLiteral(string current)
        {
            AppendToken(current);
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
                case "+":
                    return 10;
                case "(":
                    return 5;
                default:
                    return -1;
            }
        }

        private void AppendPreviousToken()
        {
            if (_previousTokens.Count > 0)
                AppendToken(_previousTokens.Pop());
        }

        private void Initialize(string expression)
        {
            TokenizeExpression(expression);
            _result = string.Empty;
            _previous = string.Empty;
            _previousTokens = new Stack<string>();
        }

        private void TokenizeExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return;

            Regex regex = new Regex(@"(\d+)|(\w+)|\+|\*|\(|\-|\)");
            Match match = regex.Match(expression);

            while (match.Success)
            {
                _tokens.Add(match.ToString());
                match = match.NextMatch();
            }
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
