package chrishenx.math_expression_system;

import java.util.ArrayList;

/*
	Automaton as tokens generator and patterns validator
	
	Z = {A,B,...,Z} U {a,b,...,z} U {0,1,...,9} U {+,-,*,^,(,)}
	states = {BEGIN, VAR, FUNC, NUM1, TRANS, NUM2, OK}
	aceptors = {VAR, FUNC, NUM1, NUM2, OK}
	
	       | [Aa-Zz] |  [0-9] |    .    |  +,-,*,^,(,)  |    
	BEGIN  |  VAR    |  NUM1  |         |      OK       |
	VAR    |  FUNC   |        |         |               |
	FUNC   |  FUNC   |        |         |               |  
	NUM1   |         |  NUM1  |  TRANS  |               |
	TRANS  |         |  NUM2  |         |               |
	NUM2   |         |  NUM2  |         |               |
	OK     |         |        |         |               |
	
	All the aceptors return to BEGIN when there are not a transition with
	the symbol currently read, and that symbol is not consumed. So, the 
	complexity of the analisys is at most F(2n) -> O(n)
	
	@author Christian González León
*/

public class Lexer {
	public String expression;
	public TerminalSymbol tokenError;
	public boolean lexicalError;
	private static final char END_OF_EXPRESSION = ' ';
	public static final String[] FUNCTIONS;
	
	static {
		FUNCTIONS = new String[] {
			"sin", "cos", "tan", "arcsin", "arccos", "arctan", // Trigonometric
            "sec", "cosec", "cotan",
            "sinh", "cosh", "tanh", // Hyperbolic
            "exp", "ln", "log", "sqrt", "abs", "cbrt", "sinc", "hstep" // Miscellaneous
		};
	}
	
	public Lexer(String expression) {
		this.expression = expression;
	}
	
	public void setExpression(String expression) {
		this.expression = expression;
	}
	
	public String getExpression() {
		return this.expression;
	}
	
	public TerminalSymbol getTokenError() {
		return this.tokenError;
	}
	
	public boolean hasLexicalError() {
		return this.lexicalError;
	}
	
	private static boolean isFunction(String str) {
		for (String func : FUNCTIONS) {
			if (func.equals(str)) {
				return true;
			}
		}
		return false;
	}
	
	private TerminalSymbol makeOneCharToken(String str, int column) {
		char symbol = str.charAt(0);
		if ("+-*/^".indexOf(symbol) != -1) {
			return new TerminalSymbol(TerminalSymbolType.Operator, column, str);
		} else if (symbol == '(') {
			return new TerminalSymbol(TerminalSymbolType.OpeningParenthesis, column, str);
		} else if (symbol == ')') {
			return new TerminalSymbol(TerminalSymbolType.ClosingParenthesis, column, str);
		} 
		return null;
	}
	
	public TerminalSymbol generateTokens() {
		final int N = expression.length();
		ArrayList<TerminalSymbol> tokens = new ArrayList<>(N);
		StringBuilder tokenValue = new StringBuilder(N);
		State currentState = State.BEGIN;
		int position = 0;
		while (position <= N && !lexicalError);
		
	}
	
	private enum State {
		BEGIN, VAR, FUNC, OK, NUM1, TRANS, NUM2
	}
	
	
}
