//=======================================================================
//  TerminalSymbolType.cs
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
    public enum TerminalSymbolType
    {
        Variable, Value, Operator, Function,
        OpeningParenthesis , ClosingParenthesis, UnRecognized 
    }
}
