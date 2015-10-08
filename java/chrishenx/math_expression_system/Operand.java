package chrishenx.math_expression_system;

public final class Operand {
	public final boolean isValue; // Otherwise is an address
	public final String value;
	
	public Operand(boolean isValue, String value) {
		this.isValue = isValue;
		this.value = value;
	}
	
	public Operand() {
		this(false, "0");
	}
}
