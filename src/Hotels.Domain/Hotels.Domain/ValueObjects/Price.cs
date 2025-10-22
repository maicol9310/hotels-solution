﻿namespace Hotels.Domain.ValueObjects
{
    public record Price
    {
        public decimal Value { get; }

        public Price(decimal value)
        {
            if (value < 0) throw new ArgumentException("Price cannot be negative", nameof(value));
            Value = value;
        }
    }
}
