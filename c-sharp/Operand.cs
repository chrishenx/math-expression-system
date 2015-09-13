using System;

namespace MathExpressionSystem
{
    public sealed class Operand
    {
        public readonly bool IsValue; // If It is not a value is an address
        public readonly String Value;

        public Operand(bool isValue, String value)
        {
            IsValue = isValue;
            Value = value;
        }

        public Operand() : this(false, "0") {}
    }
}
