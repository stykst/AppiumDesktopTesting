using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumDesktopTesting.Tests
{
    public class SevenZipBaseTest
    {
        private const string AppiumServerUri = "http://[::1]:4723/wd/hub";
        private const string AppPath = @"C:\Program Files\7-Zip\7zFM.exe";
        protected WindowsDriver<WindowsElement> driver;
        protected WindowsDriver<WindowsElement> driverDesktop;
        protected string tempDir;

        [OneTimeSetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability("app", AppPath);
            driver = new WindowsDriver<WindowsElement>
                (new Uri(AppiumServerUri), appiumOptions);

            var appiumOptionsDesktop = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptionsDesktop.AddAdditionalCapability("app", "Root");
            driverDesktop = new WindowsDriver<WindowsElement>
                (new Uri(AppiumServerUri), appiumOptionsDesktop);

            tempDir = Directory.GetCurrentDirectory() + @"\tempDir";

            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }

            Directory.CreateDirectory(tempDir);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
            driverDesktop.Quit();
        }
    }
}