using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumDesktopTesting.Tests
{
    public class SummatorBaseTest
    {
        private const string AppiumServerUri = "http://[::1]:4723/wd/hub";
        private const string AppPath = @"C:\SummatorDesktopApp.exe";
        protected WindowsDriver<WindowsElement> driver;

        [OneTimeSetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability("app", AppPath);
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServerUri), appiumOptions);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}