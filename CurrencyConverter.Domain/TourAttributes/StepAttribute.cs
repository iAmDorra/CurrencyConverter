using System;

namespace CurrencyConverter.Domain.TourAttributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Field)]
    public class StepAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }
        public int Rank { get; }

        public StepAttribute(string name, string description, int rank)
        {
            Name = name;
            Description = description;
            Rank = rank;
        }
    }
}
