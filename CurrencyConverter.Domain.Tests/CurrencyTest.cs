using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace CurrencyConverter.Domain.Tests
{
    [TestClass]
    [TestCategory("Unit test")]
    public class CurrencyTest
    {
        [TestMethod]
        public void Should_currency_be_equal_when_having_same_name()
        {
            Currency eur = new Currency("EUR");
            Currency eurCurrency = new Currency("EUR");
            Check.That(eur).IsEqualTo(eurCurrency);
        }

        [TestMethod]
        public void Should_return_true_when_currency_name_is_equal_to_the_given_name()
        {
            Currency eur = new Currency("EUR");
            var isEqual = eur.Is("EUR");
            Check.That(isEqual).IsTrue();
        }
    }
}
