using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfixToPostfix
{
    [TestClass]
    public class ShuntingYardAlgorithmTest
    {
        private string result;

        [TestMethod]
        public void EmptyExpressionResultsInSame()
        {
            Given("");
            Expect("");
        }

        void Given(string expression)
        {
            ShuntingYardAlgorithm algorithm = new ShuntingYardAlgorithm();
            this.result = algorithm.Transform(expression);
        }

        void Expect(string expected)
        {
            Assert.AreEqual(expected, this.result);
        }
    }
}
