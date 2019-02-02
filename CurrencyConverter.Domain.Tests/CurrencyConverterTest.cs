using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using NSubstitute;

namespace CurrencyConverter.Domain.Tests
{
    [TestClass]
    public class CurrencyConverterTest
    {
        [TestMethod]
        [TestCategory("Narrow Integration test")]
        public void Should_return_the_same_amount_when_target_currency_is_same()
        {
            Currency eurCurrency = new Currency("EUR");
            Amount eurAmount = new Amount(10, eurCurrency);
            IRates rates = Substitute.For<IRates>();
            Converter converter = new Converter(rates);

            Amount convertedAmount = converter.Convert(eurAmount, eurCurrency);

            Amount expectedAmount = new Amount(10, eurCurrency);
            Check.That(convertedAmount).IsEqualTo(expectedAmount);
        }

        [TestMethod]
        [TestCategory("Narrow Integration test")]
        public void Should_convert_the_amount_when_target_currency_is_different()
        {
            Currency usdCurrency = new Currency("USD");
            Currency eurCurrency = new Currency("EUR");
            Amount eurAmount = new Amount(10, eurCurrency);
            IRates rates = Substitute.For<IRates>();
            var eurUsdRate = new Rate(1.14m);
            rates.GetRateOf(usdCurrency).Returns(eurUsdRate);
            Converter converter = new Converter(rates);

            Amount convertedAmount = converter.Convert(eurAmount, usdCurrency);

            Amount expectedAmount = new Amount(11.4m, usdCurrency);
            Check.That(convertedAmount).IsEqualTo(expectedAmount);
        }

        [TestMethod]
        [TestCategory("Narrow Integration test")]
        public void Should_convert_the_amount_when_target_currency_is_different_triangulation()
        {
            Currency cadCurrency = new Currency("CAD");
            Currency eurCurrency = new Currency("EUR");
            Amount eurAmount = new Amount(10, eurCurrency);
            IRates rates = Substitute.For<IRates>();
            Rate eurCadRate = new Rate(1.5m);
            rates.GetRateOf(cadCurrency).Returns(eurCadRate);
            Converter converter = new Converter(rates);

            Amount convertedAmount = converter.Convert(eurAmount, cadCurrency);

            Amount expectedAmount = new Amount(15, cadCurrency);
            Check.That(convertedAmount).IsEqualTo(expectedAmount);
        }
    }
}
