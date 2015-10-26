//=======================================================================
//  Symbol.cs
//  math-expressions-system/c-sharp
//  Created by Christian González on 10/04/15.
//
//  Copyright © Christian González 2015.
//
//  Distributed under the MIT License.
//  (See accompanying file LICENSE or copy at
//  http://opensource.org/licenses/MIT)
//=======================================================================

namespace chrishenx.MathExpressionSystem
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
