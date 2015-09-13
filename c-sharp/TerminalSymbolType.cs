using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathExpressionSystem
{
    public enum TerminalSymbolType
    {
        Variable, Value, Operator, Function,
        OpeningParenthesis , ClosingParenthesis, UnRecognized 
    }
}
