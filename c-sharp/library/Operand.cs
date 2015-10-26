//=======================================================================
//  Operand.cs
//  math-expressions-system/c-sharp
//  Created by Christian González on 10/04/15.
//
//  Copyright © Christian González 2015.
//
//  Distributed under the MIT License.
//  (See accompanying file LICENSE or copy at
//  http://opensource.org/licenses/MIT)
//=======================================================================

using System;

namespace chrishenx.MathExpressionSystem
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
