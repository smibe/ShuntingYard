using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfixToPostfix
{
    class ShuntingYardAlgorithm
    {
        public string Transform(string expression)
        {
            string[] tokens = (expression ?? String.Empty).Split(' ');

            if (tokens.Length == 3)
            {
                return String.Format("{0} {2} {1}", tokens);
            }

            return expression ?? String.Empty;
        }
    }
}
