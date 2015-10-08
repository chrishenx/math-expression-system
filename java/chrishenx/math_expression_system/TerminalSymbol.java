package chrishenx.math_expression_system;

public final class TerminalSymbol {
	public final TerminalSymbolType type;
	public final int column;
	public final String value;
	
	public TerminalSymbol(TerminalSymbolType type, int column, String value) {
		this.type = type;
		this.column = column;
		this.value = value;
	}
	
	public TerminalSymbol(TerminalSymbolType type) {
		this(type, 0, "");
	}
	
	@Override
	public String toString() {
		switch (type)
        {
        case Variable:      
            return String.format("Variable '{0}'", value);
        case Value:
            return String.format("Value '{0}'", value);
        case Operator:
            return String.format("Operator '{0}'", value);
        case Function:
            return String.format("Function '{0}'", value);
        case OpeningParenthesis:
            return "Opening parenthesis";
        case ClosingParenthesis:
            return "Closing parenthesis";
        case UnRecognized:
            return String.format("Unrecognized '{0}'", value);
        }
        return "";
	}
	
	@Override
	public boolean equals(Object obj) {
		if (obj instanceof TerminalSymbolType) {
			TerminalSymbolType tsType = (TerminalSymbolType) obj;
			return tsType == this.type;
		}
		if (obj instanceof TerminalSymbol) {
			TerminalSymbol ts = (TerminalSymbol) obj;
			return ts.type == this.type;
		}
		return false;
	}
}
