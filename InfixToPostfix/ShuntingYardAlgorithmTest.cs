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

        [TestMethod]
        public void NullExpressionGivesEmptyResult()
        {
            Given(null);
            Expect("");
        }

        [TestMethod]
        public void JustANumberResultsInSame()
        {
            Given("74");
            Expect("74");
        }

        [TestMethod]
        public void HandlesASingleBinaryOperator()
        {
            Given("7 + 4");
            Expect("7 4 +");
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
