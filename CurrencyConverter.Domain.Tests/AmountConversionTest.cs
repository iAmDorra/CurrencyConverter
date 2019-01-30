using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace CurrencyConverter.Domain.Tests
{
    [TestClass]
    public class AmountConversionTest
    {
        [TestMethod]
        public void Should_convert_the_amount_when_given_currency_and_rate()
        {
            Currency eur = new Currency("EUR");
            Amount euroAmount = new Amount(10, eur);
            Currency usd = new Currency("USD");
            Rate eurUsdRate = new Rate(1.14);
            Amount usdAmount = euroAmount.Convert(usd, eurUsdRate);

            Amount expectedAmount = new Amount(11.4m, usd);
            Check.That(usdAmount).IsEqualTo(expectedAmount);
        }
    }

   
}