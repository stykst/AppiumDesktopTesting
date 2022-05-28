using AppiumDesktopTesting.Window;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumDesktopTesting.Tests
{
    public class SummatorTests : SummatorBaseTest
    {
        [TestCase("3", "5", "8")]
        [TestCase("3.5", "5", "8.5")]
        [TestCase("3", "5.875", "8.875")]
        [TestCase("3.14", "5.875", "9.015")]
        public void Test_SummatorTests_ValidData(string num1, string num2, string result)
        {
            var window = new SummatorWindow(driver);
            var expectedSum = double.Parse(num1) + double.Parse(num2);
            var actualSum = double.Parse(result);
            window.Calculate(num1, num2);
            var sum = double.Parse(window.ResultTextBox.Text);
            Assert.AreEqual(expectedSum, actualSum, sum);
        }

        [TestCase("", "", "error")]
        [TestCase("1", "", "error")]
        [TestCase("", "2", "error")]
        [TestCase("qer", "eqr", "error")]
        [TestCase("dsaf", "4", "error")]
        [TestCase("5", "asdf", "error")]
        public void Test_SummatorTests_InvalidData(string num1, string num2, string result)
        {
            var window = new SummatorWindow(driver);
            window.Calculate(num1, num2);

            var sum = window.ResultTextBox.Text;
            var expectedSum = result;

            Assert.AreEqual(expectedSum, sum);
        }
    }
}