namespace CurrencyConverter.Domain
{
    public class Currency
    {
        private readonly string _name;

        public Currency(string name)
        {
            _name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is Currency currency
                   && _name.Equals(currency._name);
        }

        public override int GetHashCode()
        {
            return (_name != null ? _name.GetHashCode() : 0);
        }

        public bool Is(string currencyName)
        {
            return _name.Equals(currencyName);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}