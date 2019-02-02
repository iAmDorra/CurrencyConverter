﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace CurrencyConverter.Domain.Tests
{
    [TestClass]
    public class CurrencyTest
    {
        [TestMethod]
        public void Should_currency_be_equal_when_having_same_name()
        {
            Currency eur = new Currency("EUR");
            Currency eurCurrency = new Currency("EUR");
            Check.That(eur).IsEqualTo(eurCurrency);
        }
    }
}
