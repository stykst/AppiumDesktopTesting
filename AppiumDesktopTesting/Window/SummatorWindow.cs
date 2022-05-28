using OpenQA.Selenium.Appium.Windows;

namespace AppiumDesktopTesting.Window
{
    public class SummatorWindow
    {
        private readonly WindowsDriver<WindowsElement> driver;

        public SummatorWindow(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }

        public WindowsElement FirstNumberTextBox =>
            driver.FindElementByAccessibilityId("textBoxFirstNum");
        public WindowsElement SecondNumberTextBox =>
            driver.FindElementByAccessibilityId("textBoxSecondNum");
        public WindowsElement ResultTextBox =>
            driver.FindElementByAccessibilityId("textBoxSum");
        public WindowsElement CalculateButton =>
            driver.FindElementByAccessibilityId("buttonCalc");

        public string Calculate(string num1, string num2)
        {
            FirstNumberTextBox.Clear();
            FirstNumberTextBox.SendKeys(num1);

            SecondNumberTextBox.Clear();
            SecondNumberTextBox.SendKeys(num2); 

            CalculateButton.Click();

            return ResultTextBox.Text;
        }
    }
}
