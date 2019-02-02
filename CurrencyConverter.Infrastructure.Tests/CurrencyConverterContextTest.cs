using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace CurrencyConverter.Infrastructure.Tests
{
    [TestClass]
    public class CurrencyConverterContextTest
    {
        [TestMethod]
        public void Should_insert_rates_into_database()
        {
            using (var db = new CurrencyConverterContext())
            {
                var usdRate = new RateValue { RateValueId = 1, Currency = "USD", Value = 1.14m };
                db.Rates.Add(usdRate);
                var cadRate = new RateValue { RateValueId = 2, Currency = "CAD", Value = 1.5m };
                db.Rates.Add(cadRate);
                var count = db.SaveChanges();

                Check.That(count).IsEqualTo(2);
            }
        }
    }
}
