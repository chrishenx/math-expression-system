//=======================================================================
//  Operation.cs
//  math-expressions-system/c-sharp
//  Created by Christian González on 11/04/15.
//
//  Copyright © Christian González 2015.
//
//  Distributed under the MIT License.
//  (See accompanying file LICENSE or copy at
//  http://opensource.org/licenses/MIT)
//=======================================================================

using System;
using System.Text;

namespace chrishenx.MathExpressionSystem
{
    public sealed class Operation
    {
        public readonly Operand Left, Right;
        public readonly int ResultAddress;
        public readonly DFunction Function;

        public delegate double DFunction(double left, double right);

        public Operation(Operand left, Operand right, int resultAddress, DFunction function)
        {
            Left = left;
            Right = right;
            ResultAddress = resultAddress;
            Function = function;
        }

        public Operation(Operand operand, int resultAddress, DFunction function)
            : this(operand, null, resultAddress, function) {}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(50);
            sb.Append(String.Format(Left.IsValue ? "v({0})" : "d({0})", Left.Value));
            if (Right != null)
            {
                sb.Append(" - ");
                sb.Append(String.Format(Right.IsValue ? "v({0})" : "d({0})", Right.Value));
            }
            sb.Append(String.Format(" ->  {0}", ResultAddress));
            return sb.ToString();
        }

        // Do not see the code that follows, it is a necesary bad

        public static DFunction MakeFunction(string function) 
        {
            if (function.Equals("u+"))
            {
                return delegate(double a, double b /*unused*/) {
                    return +a;
                };
            }
            else if (function.Equals("u-"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return -a;
                };
            }
            else if (function.Equals("b+"))
            {
                return delegate(double a, double b)
                {
                    return a + b;
                };
            }
            else if (function.Equals("b-"))
            {
                return delegate(double a, double b)
                {
                    return a - b;
                };
            }
            else if (function.Equals("*"))
            {
                return delegate(double a, double b)
                {
                    return a * b;
                };
            }
            else if (function.Equals("/"))
            {
                return delegate(double a, double b)
                {
                    if (b == 0)
                    {
                        throw new DivideByZeroException("Division by zero");
                    }
                    return a / b;
                };
            }
            else if (function.Equals("^"))
            {
                return delegate(double a, double b)
                {
                    return Math.Pow(a, b);
                };
            }
            else if (function.Equals("sin"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Sin(a);
                };
            }
            else if (function.Equals("cos"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Cos(a);
                };
            }
            else if (function.Equals("tan"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Tan(a);
                };
            }
            else if (function.Equals("arcsin"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    if (a > 1 || a < -1)
                    {
                        throw new ArgumentOutOfRangeException("arcsin is just defined at [-1, 1]");
                    }
                    return Math.Asin(a);
                };
            }
            else if (function.Equals("arccos"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    if (a > 1 || a < -1)
                    {
                        throw new ArgumentOutOfRangeException("arccos is just defined at [-1, 1]");
                    }
                    return Math.Acos(a);
                };
            }
            else if (function.Equals("arctan"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Atan(a);
                };
            }
            else if (function.Equals("sec"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return 1 / Math.Cos(a);
                };
            }
            else if (function.Equals("cosec"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return 1 / Math.Sin(a);
                };
            }
            else if (function.Equals("cotan"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return 1 / Math.Tan(a);
                };
            }
            else if (function.Equals("sinh"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Sinh(a);
                };
            }
            else if (function.Equals("cosh"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Cosh(a);
                };
            }
            else if (function.Equals("tanh"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Tanh(a);
                };
            }
            else if (function.Equals("exp"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Exp(a);
                };
            }
            else if (function.Equals("ln"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Log(a);
                };
            }
            else if (function.Equals("log"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Log10(a);
                };
            }
            else if (function.Equals("sqrt"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Sqrt(a);
                };
            }
            else if (function.Equals("sinc"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return Math.Sin(a) / a;
                };
            }
            else if (function.Equals("hstep"))
            {
                return delegate(double a, double b /*unused*/)
                {
                    return a >= 0 ? 1 : 0;
                };
            }
            throw new ArgumentException("No such function: " + function);
        }
    }
}
