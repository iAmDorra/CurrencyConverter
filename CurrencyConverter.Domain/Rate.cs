namespace CurrencyConverter.Domain
{
    public class Rate
    {
        private readonly decimal _rate;

        public Rate(decimal rate)
        {
            _rate = rate;
        }

        public decimal Multiply(decimal value)
        {
            return _rate * value;
        }

        public override bool Equals(object obj)
        {
            return obj is Rate rate
                   && rate._rate.Equals(_rate);
        }
        
        public override int GetHashCode()
        {
            return _rate.GetHashCode();
        }
    }
}