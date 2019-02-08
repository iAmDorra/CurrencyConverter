using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
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
            using (var driver = new EdgeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44304/Home");
                Thread.Sleep(1000);
                var amountValue = "100";
                SetValueToElement(driver, amountValue, "Amount");

                var currencyValue = "USD";
                SetValueToElement(driver, currencyValue, "Currency");

                Thread.Sleep(1000);

                var convertedAmount = driver.FindElement(By.Id("ConvertedAmount"));
                string convertedAmountValue = GetValue(convertedAmount);
                Check.That(convertedAmountValue).IsEqualTo("114,00 USD");
            }
        }

        private static void SetValueToElement(EdgeDriver driver, string elementValue, string elementId)
        {
            var element = driver.FindElement(By.Id(elementId));
            element.Clear();
            element.SendKeys(elementValue);
            element.SendKeys(Keys.Enter);
            Check.That(GetValue(element)).IsEqualTo(elementValue);
        }

        private static string GetValue(IWebElement convertedAmount)
        {
            return convertedAmount.GetAttribute("value");
        }
    }
}
