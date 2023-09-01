﻿Feature: CurrencyConversion
Simple converter from an amount to target currency
Amount(ccy) + target ccy => Amount(target ccy)

Link to a feature: [Calculator](CurrencyConverter.AcceptanceTests/Features/CurrencyConversion.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: Simple conversion
	Given the EUR to USD exchange rate is 1.1329
	When I convert 10 EUR to USD by rounding to cents
	Then I get 11.33 USD
