using CurrencyConverter.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CurrencyConverter.App
{
    public class Startup
    {
        public static void InitializeDatabase()
        {
            MigrateDatabase();
            CleanRatesTable();
            InsertNewRate("USD", 1.14m);
        }
        private static void MigrateDatabase()
        {
            using (var dbContext = new CurrencyConverterContext())
            {
                dbContext.Database.Migrate();
            }
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
                FormattableString sql = FormattableStringFactory.Create(deleteQuery);
                dbContext.Database.ExecuteSql(sql);
            }
        }
    }
}
