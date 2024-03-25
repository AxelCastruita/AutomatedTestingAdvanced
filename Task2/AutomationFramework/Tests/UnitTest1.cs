using AutomationFramework.pageObjects.ReportPortal;
using AutomationFramework.Utilities;
using OpenQA.Selenium;

namespace AutomationFramework.Tests
{
    public class Tests : Base
    {      
        

        [Test]
        public void Test1()
        {

            Assert.Pass();
        }

        [TestCase("Hello","World")]
        [TestCase("Hello","Automation")]
        public void Test2(string word1,string word2)
        {
            Assert.Pass(word1,word2);
        }

        [TestCaseSource(nameof(AddTestData))]
        public void Test3(string word1, string word2)
        {
            Assert.Pass(word1, word2);
        }

        [TestCaseSource(nameof(jsonTestData))]
        public void Test4(string word1, string word2)
        {
            Assert.That(word1, Is.EqualTo("AxelCastruita"));
            Assert.That(word2, Is.EqualTo("Hello123"));
        }

        [Test]
        public void Test5()
        {
            Assert.Fail();
        }


        public static IEnumerable<TestCaseData> AddTestData()
        {
            yield return new TestCaseData("Hello", "Data");
            yield return new TestCaseData("Hello", "samples");
            yield return new TestCaseData("Hello", "provided");

        }

        public static IEnumerable<TestCaseData> jsonTestData()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"));          
        }
    }
}