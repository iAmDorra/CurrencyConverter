using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace CurrencyConverter.Domain.Tests
{
    [TestClass]
    public class CurrencyConverterTest
    {
        [TestMethod]
        public void Should_return_the_same_amount_when_target_currency_is_same()
        {
            Currency eurCurrency = new Currency("EUR");
            Amount eurAmount = new Amount(10, eurCurrency);
            Converter converter = new Converter();

            Amount convertedAmount = converter.Convert(eurAmount, eurCurrency);

            Amount expectedAmount = new Amount(10, eurCurrency);
            Check.That(convertedAmount).IsEqualTo(expectedAmount);
        }
    }
}
