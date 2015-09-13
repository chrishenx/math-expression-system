using System;

namespace MathExpressionSystem
{
    public sealed class TerminalSymbol
    {
        public readonly TerminalSymbolType Type;
        public readonly int Column;
        public readonly string Value;

        public TerminalSymbol(TerminalSymbolType type, int column, string value)
        {
            Type = type;
            Column = column;
            Value = value;
        }

        public TerminalSymbol(TerminalSymbolType type)
            : this(type, 0, "") { }

        public override string ToString()
        {
            switch (Type)
            {
                case TerminalSymbolType.Variable:      
                  return String.Format("Variable '{0}'", Value);
                case TerminalSymbolType.Value:
                  return String.Format("Value '{0}'", Value);
                case TerminalSymbolType.Operator:
                  return String.Format("Operator '{0}'", Value);
                case TerminalSymbolType.Function:
                  return String.Format("Function '{0}'", Value);
                case TerminalSymbolType.OpeningParenthesis:
                  return "Opening parenthesis";
                case TerminalSymbolType.ClosingParenthesis:
                  return "Closing parenthesis";
                case TerminalSymbolType.UnRecognized:
                  return String.Format("Unrecognized '{0}'", Value);
            }
            return "";
        }

        public static bool operator==(TerminalSymbol terminalSymbol, TerminalSymbolType type) 
        {
            if ((Object)terminalSymbol == null)
            {
                return false;
            }
            return terminalSymbol.Type == type;
        }

        public static bool operator !=(TerminalSymbol terminalSymbol, TerminalSymbolType type)
        {
            return !(terminalSymbol == type);
        }

        public override bool Equals(Object obj)
        {
            if (!Object.ReferenceEquals(null, obj) && obj.GetType() == typeof(TerminalSymbol))
            {
                return Type == (TerminalSymbolType)obj;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
