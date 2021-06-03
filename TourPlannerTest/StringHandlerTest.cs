using NUnit.Framework;
using System;
using System.Collections.Generic;
using TourPlannerBL;

namespace TourPlannerTest
{
    [TestFixture]
    public class StringHandlerTest
    {
        private StringHandler strHandler = new();
        [Test]
        public void StringSplitterTest()
        {
            string TestString = "This-Test";
            List<String> TestList = new();
            TestList.Add("This");
            TestList.Add("Test");
            Assert.AreEqual(strHandler.StringSplitter(TestString), TestList);
        }
        [Test]
        public void StringValidationTestWrongInput()
        {
            string TestString = "This-Test";
            Assert.IsFalse(strHandler.StringValidation(TestString));
        }
        [Test]
        public void StringValidationTestCorrectInput()
        {
            string TestString = "Rainy";
            Assert.IsTrue(strHandler.StringValidation(TestString));
        }
        [Test]
        public void StringValidationWithDigitsTestWrongInput()
        {
            string TestString = "This-Test";
            Assert.IsFalse(strHandler.StringValidationWithDigits(TestString));
        }
        [Test]
        public void StringValidationWithDigitsTestCorrectInput()
        {
            string TestString = "1200Vienna";
            Assert.IsTrue(strHandler.StringValidationWithDigits(TestString));
        }
    }
}
