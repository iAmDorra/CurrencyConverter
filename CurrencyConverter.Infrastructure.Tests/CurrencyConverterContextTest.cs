using CurrencyConverter.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace CurrencyConverter.Infrastructure.Tests
{
    [TestClass]
    public class CurrencyConverterContextTest
    {
        const string usdCurrencyName = "USD";
        const decimal usdRateValue = 1.14m;

        [TestInitialize]
        public void Initialize()
        {
            CleanRatesTable();
            InsertNewRate(usdCurrencyName, usdRateValue);
        }

        [TestCleanup]
        public void CleanUp()
        {
            CleanRatesTable();
        }

        [TestMethod]
        [TestCategory("Broad Integration test")]
        public void Should_convert_the_amount_when_target_currency_is_different_triangulation()
        {
            Currency usdCurrency = new Currency("USD");
            Currency eurCurrency = new Currency("EUR");
            Amount eurAmount = new Amount(10, eurCurrency);
            IRates rates = new Rates();
            Converter converter = new Converter(rates);

            Amount convertedAmount = converter.Convert(eurAmount, usdCurrency);

            Amount expectedAmount = new Amount(11.4m, usdCurrency);
            Check.That(convertedAmount).IsEqualTo(expectedAmount);
        }

        [TestMethod]
        [TestCategory("Broad Integration test")]
        public void Should_return_rates_saved_in_database()
        {
            var rates = new Rates();
            Currency usdCurrency = new Currency(usdCurrencyName);
            var usdEurRate = rates.GetRateOf(usdCurrency);
            Rate expectedRate = new Rate(usdRateValue);
            Check.That(usdEurRate).IsEqualTo(expectedRate);
        }

        private static void InsertNewRate(string usdCurrencyName, decimal usdRateValue)
        {
            using (var dbContext = new CurrencyConverterContext())
            {
                var usdRate = new RateValue { RateValueId = 1, Currency = usdCurrencyName, Value = usdRateValue };
                dbContext.Rates.Add(usdRate);
                dbContext.SaveChanges();
            }
        }

        private static void CleanRatesTable()
        {
            using (var dbContext = new CurrencyConverterContext())
            {
                var deleteQuery = $"delete from {nameof(dbContext.Rates)}";
                dbContext.Database.ExecuteSqlCommand(deleteQuery);
            }
        }
    }
}
