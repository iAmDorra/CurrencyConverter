using FsCheck;
using FsCheck.Xunit;
using System;

namespace CurrencyConverter.Domain.PropertyBasedTests
{
    public class AmountConversionPropertyBasedTest
    {
        [Property]
        public Property Should_convert_the_amount_using_rate(decimal amountValue, decimal rate)
        {
            Func<bool> codeToCheck = () =>
            {
                Currency eur = new Currency("EUR");
                Amount euroAmount = new Amount(amountValue, eur);
                Currency usd = new Currency("USD");
                Rate eurUsdRate = new Rate(rate);
                Amount usdAmount = euroAmount.Convert(usd, eurUsdRate);
                return usdAmount.HasCurrency(usd) && usdAmount.HasValue(amountValue * rate);
            };

            return codeToCheck.When(
                rate != 0
                && rate > 1
                && rate < 100000000
                && amountValue > decimal.MinValue / rate
                && amountValue < decimal.MaxValue / rate);
        }
    }
}
