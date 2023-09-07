using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using NFluent;
using OpenQA.Selenium.Edge;

namespace CurrencyConverter.Web.Tests
{
    [TestClass]
    public class SeleniumTest
    {
        [TestMethod]
        [TestCategory("End to end test")]
        public void Should_return_the_right_conversion_when_using_Edge_driver()
        {
            using (var driver = new EdgeDriver())
            {
                driver.Navigate().GoToUrl(@"https://localhost:44346/Home");
                Thread.Sleep(1000);
                var amountValue = "100";
                SetValueToElement(driver, amountValue, "Amount");

                var currencyValue = "USD";
                SetValueToElement(driver, currencyValue, "Currency");

                var convertedAmount = driver.FindElement(By.Id("ConvertedAmount"));
                string convertedAmountValue = GetValue(convertedAmount);
                Check.That(convertedAmountValue).IsEqualTo("114.00 USD");

                CreateDocumentation(driver);
            }
        }

        private static void CreateDocumentation(EdgeDriver driver)
        {
            var screenShot = driver as ITakesScreenshot;
            var screen = screenShot.GetScreenshot();
            screen.SaveAsFile(@".\converter.png", ScreenshotImageFormat.Png);
        }

        private static void SetValueToElement(EdgeDriver driver, string elementValue, string elementId)
        {
            var element = driver.FindElement(By.Id(elementId));
            element.Clear();
            element.SendKeys(elementValue);
            element.SendKeys(Keys.Enter);
        }

        private static string GetValue(IWebElement convertedAmount)
        {
            return convertedAmount.GetAttribute("value");
        }
    }
}
