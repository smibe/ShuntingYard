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

        [TestMethod]
        public void ParsesExpressionsWithoutSpaces()
        {
            Given("1+2");
            Expect("1 2 +");
        }

        [TestMethod]
        public void HandlesMultipleOperatorsOfTheSamePrecedence()
        {
            Given("7+4-    8");
            Expect("7 4 + 8 -");
        }

        [TestMethod]
        public void HandlesOperatorsOfDifferentPrecedence()
        {
            Given("7 + 4 * 8");
            Expect("7 4 8 * +");
        }

        [TestMethod]
        public void RemoveUnnecessaryParanthesis()
        {
            Given("( 2 * 4 ) + 8 )");
            Expect("2 4 * 8 +");
        }

        [TestMethod]
        public void HandlesOperatorsLowHighHighLowLow()
        {
            Given("3 + 3 * 5 * 4 + 6 + 7");
            Expect("3 3 5 * 4 * + 6 + 7 +");
        }

        [TestMethod]
        public void HandlesPharantesisCorrectly()
        {
            Given("( ( a + 5 ) * 9 )");
            Expect("a 5 + 9 *");
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
