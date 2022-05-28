using AppiumDesktopTesting.Window;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumDesktopTesting.Tests
{
    public class SevenZipTests
    {
        private const string AppiumServerUri = "http://[::1]:4723/wd/hub";
        private const string FilePath = @"C:\Program Files\7-Zip\";
        private const string AppPath = FilePath + "7zFM.exe";
        private WindowsDriver<WindowsElement> driver;
        protected WindowsDriver<WindowsElement> desktopDriver;
        protected string workDir;

        [OneTimeSetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability("app", AppPath);
            driver = new WindowsDriver<WindowsElement>
                (new Uri(AppiumServerUri), appiumOptions);

            var appiumOptionsDesktop = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptionsDesktop.AddAdditionalCapability("app", "Root");
            desktopDriver = new WindowsDriver<WindowsElement>
                (new Uri(AppiumServerUri), appiumOptionsDesktop);

            workDir = Directory.GetCurrentDirectory() + @"\tempDir";

            if (Directory.Exists(workDir))
            {
                Directory.Delete(workDir, true);
            }

            Directory.CreateDirectory(workDir);
        }
        
        [Test]
        public void Test_SevenZip()
        {
            var comboBoxLocation = driver
                .FindElementByClassName("Edit");

            Thread.Sleep(1000);

            comboBoxLocation.SendKeys(FilePath);
            comboBoxLocation.SendKeys(Keys.Enter);

            var listBoxFiles = driver
                .FindElementByClassName("SysListView32");
            listBoxFiles.SendKeys(Keys.Control + 'a');

            var buttonAdd = driver
                .FindElementByName("Add");
            buttonAdd.Click();

            Thread.Sleep(1000);

            var windowAddToArchive =
                desktopDriver.FindElementByName("Add to Archive");

            var comboBoxArhiveName = windowAddToArchive
                .FindElementByXPath("/Window/ComboBox/Edit[@Name='Archive:']");
            var archiveFileName = workDir +@"\" + DateTime.Now.Ticks + ".7z";
            comboBoxArhiveName.SendKeys(archiveFileName);

            var comboBoxArhiveFormat = windowAddToArchive
                .FindElementByXPath("/Window/ComboBox[@Name='Archive format:']");
            comboBoxArhiveFormat.SendKeys("7z");

            var comboBoxCompressionLevel = windowAddToArchive
                .FindElementByXPath("/Window/ComboBox[@Name='Compression level:']");
            comboBoxCompressionLevel.SendKeys("Ultra");

            var comboBoxCompressionMethod = windowAddToArchive
                .FindElementByXPath("/Window/ComboBox[@Name='Compression method:']");
            comboBoxCompressionMethod.SendKeys(Keys.Home);

            var comboBoxDictionarySize = windowAddToArchive
                .FindElementByXPath("/Window/ComboBox[@Name='Dictionary size:']");
            comboBoxDictionarySize.SendKeys(Keys.End);

            var comboBoxWordSize = windowAddToArchive
                .FindElementByXPath("/Window/ComboBox[@Name='Word size:']");
            comboBoxWordSize.SendKeys(Keys.End);

            var buttonCreateArchiveOK = windowAddToArchive
                .FindElementByXPath("/Window/Button[@Name='OK']");
            buttonCreateArchiveOK.Click();

            Thread.Sleep(1000);

            // Extract archive
            comboBoxLocation.SendKeys(archiveFileName + Keys.Enter);

            var buttonExtract = driver
                .FindElementByName("Extract");
            buttonExtract.Click();

            var buttonExtractOK = driver
                .FindElementByName("OK");
            buttonExtractOK.Click();

            Thread.Sleep(1000);

            //Assertions
            //Assert the originals files are the same as the extracted files

            var original7ZipExe = FilePath + "7zFM.exe";
            var extracted7ZipExe = workDir + @"\7zFM.exe";

            FileAssert.AreEqual(original7ZipExe, extracted7ZipExe);

            foreach (string originalFile in Directory
                .EnumerateFiles(FilePath,
                "*.*", SearchOption.AllDirectories))
            {
                var originalFileName = originalFile.Replace(FilePath, "");
                var fileCopy = workDir + @"\" + originalFileName;
                FileAssert.AreEqual(fileCopy, originalFile);
            }
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
            desktopDriver.Quit();
        }
    }
}