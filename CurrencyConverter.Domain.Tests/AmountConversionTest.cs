using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using NSubstitute;

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

        [TestMethod]
        [TestCategory("Unit test")]
        public void Should_format_amount_when_a_formatter_is_given()
        {
            Currency eur = new Currency("EUR");
            Amount euroAmount = new Amount(10, eur);
            var formatter = Substitute.For<IAmountFormatter>();
            string formattedAmount = "10 EUR";
            formatter.Format(Arg.Any<decimal>(), Arg.Any<Currency>()).Returns(formattedAmount);

            string stringAmountValue = euroAmount.Format(formatter);

            Check.That(stringAmountValue).IsEqualTo(formattedAmount);
        }

        [TestMethod]
        [TestCategory("Broad Integration test")]
        public void Should_format_amount_when_a_reel_formatter_is_given()
        {
            Currency eur = new Currency("EUR");
            Amount euroAmount = new Amount(10, eur);
            var formatter = new AmountFormatter();
            
            string stringAmountValue = euroAmount.Format(formatter);

            string formattedAmount = "10 EUR";
            Check.That(stringAmountValue).IsEqualTo(formattedAmount);
        }

        [TestMethod]
        [TestCategory("Unit test")]
        public void Should_return_null_if_no_rate_found()
        {
            Currency usdCurrency = new Currency("USD");
            Currency eurCurrency = new Currency("EUR");
            Amount eurAmount = new Amount(10, eurCurrency);
            Rate nullRate = null;
            
            Amount convertedAmount = eurAmount.Convert(usdCurrency, nullRate);

            Check.That(convertedAmount).IsNull();
        }
    }
}
