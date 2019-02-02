using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace CurrencyConverter.Domain.Tests
{
    [TestClass]
    public class AmountConversionTest
    {
        [TestMethod]
        [TestCategory("Unit test")]
        public void Should_convert_the_amount_when_given_currency_and_rate()
        {
            Currency eur = new Currency("EUR");
            Amount euroAmount = new Amount(10, eur);
            Currency usd = new Currency("USD");
            Rate eurUsdRate = new Rate(1.14m);
            Amount usdAmount = euroAmount.Convert(usd, eurUsdRate);

            Amount expectedAmount = new Amount(11.4m, usd);
            Check.That(usdAmount).IsEqualTo(expectedAmount);
        }

        [TestMethod]
        [TestCategory("Unit test")]
        public void Should_convert_the_amount_when_given_currency_and_rate_triangulation()
        {
            Currency eur = new Currency("EUR");
            Amount euroAmount = new Amount(1, eur);
            Currency usd = new Currency("USD");
            Rate eurUsdRate = new Rate(1.14m);
            Amount usdAmount = euroAmount.Convert(usd, eurUsdRate);

            Amount expectedAmount = new Amount(1.14m, usd);
            Check.That(usdAmount).IsEqualTo(expectedAmount);
        }

        [TestMethod]
        [TestCategory("Unit test")]
        public void Should_return_the_same_amount_when_target_currency_is_same()
        {
            Currency eur = new Currency("EUR");
            Amount euroAmount = new Amount(10, eur);
            Rate eurRate = new Rate(1.14m);
            Amount convertedAmount = euroAmount.Convert(eur, eurRate);
            
            Check.That(convertedAmount).IsEqualTo(euroAmount);
        }

        [TestMethod]
        [TestCategory("Unit test")]
        public void Should_return_the_same_amount_when_target_currency_name_is_same()
        {
            Currency eur = new Currency("EUR");
            Amount euroAmount = new Amount(10, eur);
            Rate eurRate = new Rate(1.14m);
            Currency eurCurrency = new Currency("EUR");
            Amount convertedAmount = euroAmount.Convert(eurCurrency, eurRate);
            
            Check.That(convertedAmount).IsEqualTo(euroAmount);
        }
    }
}
