using CurrencyConverter.Domain;
using NSubstitute;
using NUnit.Framework;

namespace CurrencyConverter.AcceptanceTests.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private IRates rates = Substitute.For<IRates>();
        private Amount convertedAmount;

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        [Given("the (.*) to (.*) exchange rate is (.*)")]
        public void GivenTheExchangeRateIs(string sourceCurrency, string targetCurrency, decimal rate)
        {
            Rate eurRate = new Rate(rate);
            Currency currency = new Currency(targetCurrency);
            rates.GetRateOf(currency).Returns(eurRate);
        }

        [When("I convert (.*) (.*) to (.*) by (.*)")]
        public void WhenIConvertTo(decimal amount, string sourceCurrency, string targetCurrency, Rounding rounding)
        {
            Currency amountCurrency = new(sourceCurrency);
            Amount initialAmount = new(amount, amountCurrency);

            Currency currency = new(targetCurrency);
            Converter converter = new(rates);
            convertedAmount = converter.Convert(initialAmount, currency, rounding);
        }

        [Then("I get (.*) (.*)")]
        public void ThenIGetByRounding(decimal amount, string currency)
        {
            Currency expectedCurrency = new Currency(currency);
            Amount expectedAmount = new Amount(amount, expectedCurrency);
            Assert.AreEqual(expectedAmount, convertedAmount);
        }
    }

    [Binding]
    public class Transforms
    {
        [StepArgumentTransformation(@"rounding (.*)")]
        public Rounding InXDaysTransform(string round)
        {
            switch(round)
            {
                case "to cents":
                    return Rounding.ToCents;
                case "to units":
                    return Rounding.ToUnits;
            }

            return Rounding.ToCents;
        }
    }


}