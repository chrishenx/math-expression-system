package chrishenx.math_expression_system;

public final class Symbol {
	public final boolean isTerminal;
	public final TerminalSymbolType terminalSymbol;
	public final NonTerminalSymbol nonTerminalSymbol;
	
	public Symbol(TerminalSymbolType terminalSymbol) {
		this.isTerminal = true;
		this.terminalSymbol = terminalSymbol;
		this.nonTerminalSymbol = null;
	}
	
	public Symbol(NonTerminalSymbol nonTerminalSymbol) {
		this.isTerminal = false;
		this.nonTerminalSymbol = nonTerminalSymbol;
		this.terminalSymbol = null;
	}
}
