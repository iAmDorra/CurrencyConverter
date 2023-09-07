using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using System;
using System.Net;

namespace CurrencyConverter.Web.Tests
{
    [TestClass]
    public class HealthTest
    {
        [TestMethod]
        [TestCategory("Health check test")]
        public void Check_that_web_site_is_up()
        {
            const string url = @"http://localhost:44346";
            bool isServiceUp;
            try
            {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)myRequest.GetResponse();
                isServiceUp = response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                isServiceUp = false;
            }

            Check.That(isServiceUp).IsTrue();
        }
    }
}
