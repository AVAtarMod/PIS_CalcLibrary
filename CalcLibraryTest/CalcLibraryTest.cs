using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CalcLibrary;

namespace CalcLibraryTest
{
    [TestClass]
    public class CalcLibraryTest
    {
        private const string operands1 = "23+4,5";
        private const string operands2 = "-23+-4,5";

        [TestMethod]
        public void GetOperandsPositiveTestMethod()
        {
            String[] a = Calc.GetOperands(operands1);

            Assert.AreEqual("23", a[0]);
            Assert.AreEqual("4,5", a[1]);
        }
        [TestMethod]
        public void GetOperandsNegativeTestMethod()
        {
            String[] a = Calc.GetOperands(operands2);

            Assert.AreEqual("-23", a[0]);
            Assert.AreEqual("-4,5", a[1]);
        }
        [TestMethod]
        public void GetOperationTestMethod()
        {
            String a = Calc.GetOperation(operands2);

            Assert.AreEqual("+", a);
        }
        [TestMethod]
        public void ResultTestMethod()
        {
            string result = Calc.DoOperation("23+4,5");

            Assert.AreEqual((27.5D).ToString(), Calc.DoubleOperation["+"](23D, 4.5).ToString());
            Assert.AreEqual((27.5D).ToString(), result);
        }
    }
}
