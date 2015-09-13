
namespace MathExpressionSystem
{
    sealed class Symbol
    {
        public readonly bool IsTerminal;
        public readonly TerminalSymbolType TerminalSymbol;
        public readonly NonTerminalSymbol NonTerminalSymbol;

        public Symbol(TerminalSymbolType terminalSymbol)
        {
            IsTerminal = true;
            TerminalSymbol = terminalSymbol;
        }

        public Symbol(NonTerminalSymbol nonTerminalSymbol)
        {
            IsTerminal = false;
            NonTerminalSymbol = nonTerminalSymbol;
        }
    }
}
