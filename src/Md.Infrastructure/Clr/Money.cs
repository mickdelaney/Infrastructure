using System;

namespace Md.Infrastructure.Clr
{
    public struct Money
    {
        private readonly string _currency;
        private readonly int _cents;
        private readonly double _roundedAmount;

        public Money(double amount): this(amount, "GBP"){}
        public Money(double amount, string currency)
        {
            _currency = currency;
            _roundedAmount = RoundToNearestPenny(amount);
            _cents = ToPennies(_roundedAmount);
        }

        public string Currency
        {
            get { return _currency; }
        }
        public double Amount
        {
            get { return _roundedAmount; }
        }
        public int Cents
        {
            get { return _cents; }
        }

        private static int ToPennies(double amount)
        {
            return Convert.ToInt32(amount*100);
        }
        private static double RoundToNearestPenny(double amount)
        {
            double quotient = amount/.01;
            var wholePart = (int) quotient;
            decimal mantissa = ((decimal) quotient) - wholePart;

            return mantissa >= .5m ? .01*(wholePart + 1) : .01*wholePart;
        }
        
        public Money Add(Money other)
        {
            return new Money(Amount + other.Amount);
        }
        public Money MultiplyBy(double multiplicationFactor)
        {
            return new Money(Amount * multiplicationFactor);
        }
    }
}