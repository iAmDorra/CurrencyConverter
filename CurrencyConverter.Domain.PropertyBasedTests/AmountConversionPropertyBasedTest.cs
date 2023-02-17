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
                return usdAmount.HasValue(amountValue * rate);
            };

            return codeToCheck.When(Global.AnyInput);
        }

        [Property]
        public Property Result_amount_should_be_in_target_currency(decimal amountValue, decimal rate)
        {
            return Prop.When(Global.AnyInput,
                  () =>
                  {
                      Currency eur = new Currency("EUR");
                      Amount euroAmount = new Amount(amountValue, eur);
                      Currency usd = new Currency("USD");
                      Rate eurUsdRate = new Rate(rate);
                      Amount usdAmount = euroAmount.Convert(usd, eurUsdRate);
                      return usdAmount.HasCurrency(usd);
                  });
        }
    }

    public class Global
    {
        public static bool AnyInput = true;
    }
}
