using System;
using System.Collections.Generic;
using System.Linq;

namespace MathExpressionSystem
{
    public sealed class Parser
    {
        private List<TerminalSymbol> Tokens;
        public TerminalSymbol TokenError { get; private set; }
        public bool SyntaxError { get; private set; }

        public Parser(List<TerminalSymbol> tokens)
        {
            Tokens = tokens;
        }

        public void ValidateSintax()
        {
            var stack = new Stack<Symbol>();
            stack.Push(new Symbol(NonTerminalSymbol.E));
            int N = Tokens.Count;
            int position = 0;
            while (stack.Count > 0)
            {
                Symbol current = stack.Pop();
                if (!current.IsTerminal) 
                {
                    NonTerminalSymbol symbolValue = current.NonTerminalSymbol;
                    Console.Out.Write(symbolValue);
                    if (symbolValue == NonTerminalSymbol.E) 
                    {
                        Console.Out.WriteLine("  -> TX");
                        stack.Push(new Symbol(NonTerminalSymbol.X));
                        stack.Push(new Symbol(NonTerminalSymbol.T));
                    } 
                    else if (symbolValue == NonTerminalSymbol.T) 
                    {
                        if (Tokens[position] == TerminalSymbolType.Function) 
                        {
                            Console.Out.WriteLine("  -> F(E)X");
                            stack.Push(new Symbol(NonTerminalSymbol.X));
                            stack.Push(new Symbol(TerminalSymbolType.ClosingParenthesis));            
                            stack.Push(new Symbol(NonTerminalSymbol.E));
                            stack.Push(new Symbol(TerminalSymbolType.OpeningParenthesis));            
                            stack.Push(new Symbol(TerminalSymbolType.Function));            
                        } 
                        else if (Tokens[position] == TerminalSymbolType.OpeningParenthesis) 
                        {
                            Console.Out.WriteLine("  -> (E)X");
                            stack.Push(new Symbol(NonTerminalSymbol.X));
                            stack.Push(new Symbol(TerminalSymbolType.ClosingParenthesis));            
                            stack.Push(new Symbol(NonTerminalSymbol.E));            
                            stack.Push(new Symbol(TerminalSymbolType.OpeningParenthesis));    
                        } 
                        else if (Tokens[position] == TerminalSymbolType.Value) 
                        {
                            Console.Out.WriteLine("  -> valueX");
                            stack.Push(new Symbol(NonTerminalSymbol.X));
                            stack.Push(new Symbol(TerminalSymbolType.Value));
                        } 
                        else if (Tokens[position] == TerminalSymbolType.Variable) 
                        {
                            Console.Out.WriteLine("  -> varX");
                            stack.Push(new Symbol(NonTerminalSymbol.X));
                            stack.Push(new Symbol(TerminalSymbolType.Variable));            
                        } 
                        else if ("+-".Contains(Tokens[position].Value[0])) 
                        {
                            Console.Out.WriteLine("  -> {0}U", Tokens[position].Value);
                            stack.Push(new Symbol(NonTerminalSymbol.U));
                            stack.Push(new Symbol(TerminalSymbolType.Operator));
                        } 
                        else  
                        {
                            var errorMsg = String.Format("Unexpected token '{0}'", 
                                Tokens[position].Value);
                            TokenError = new TerminalSymbol(Tokens[position].Type, 
                                Tokens[position].Column, errorMsg);
                            SyntaxError = true;
                            break;
                        }
                    } 
                    else if (symbolValue == NonTerminalSymbol.X) 
                    {
                        if (position >= N)  
                        {
                            Console.Out.WriteLine("  -> eps");
                        } 
                        else if (Tokens[position] == TerminalSymbolType.Operator) 
                        {
                            Console.Out.WriteLine("  -> {0}E", Tokens[position].Value);
                            stack.Push(new Symbol(NonTerminalSymbol.U));
                            stack.Push(new Symbol(TerminalSymbolType.Operator));
                        } 
                        else if (Tokens[position] != TerminalSymbolType.ClosingParenthesis) 
                        {
                            var errorMsg = String.Format("Unexpected token '{0}'", 
                                Tokens[position].Value);
                            TokenError = new TerminalSymbol(Tokens[position].Type, 
                                Tokens[position].Column, errorMsg);
                            SyntaxError = true;
                            break;
                        } 
                        else  
                        {   // Reading a closing parenthesis
                            Console.Out.WriteLine("  -> eps");
                        }
                    }
                    else if (symbolValue == NonTerminalSymbol.U)
                    {
                        if (Tokens[position] == TerminalSymbolType.Function)
                        {
                            Console.Out.WriteLine("  -> F(E)X");
                            stack.Push(new Symbol(NonTerminalSymbol.X));
                            stack.Push(new Symbol(TerminalSymbolType.ClosingParenthesis));
                            stack.Push(new Symbol(NonTerminalSymbol.E));
                            stack.Push(new Symbol(TerminalSymbolType.OpeningParenthesis));
                            stack.Push(new Symbol(TerminalSymbolType.Function));
                        }
                        else if (Tokens[position] == TerminalSymbolType.OpeningParenthesis)
                        {
                            Console.Out.WriteLine("  -> (E)X");
                            stack.Push(new Symbol(NonTerminalSymbol.X));
                            stack.Push(new Symbol(TerminalSymbolType.ClosingParenthesis));
                            stack.Push(new Symbol(NonTerminalSymbol.E));
                            stack.Push(new Symbol(TerminalSymbolType.OpeningParenthesis));
                        }
                        else if (Tokens[position] == TerminalSymbolType.Value)
                        {
                            Console.Out.WriteLine("  -> valueX");
                            stack.Push(new Symbol(NonTerminalSymbol.X));
                            stack.Push(new Symbol(TerminalSymbolType.Value));
                        }
                        else if (Tokens[position] == TerminalSymbolType.Variable)
                        {
                            Console.Out.WriteLine("  -> varX");
                            stack.Push(new Symbol(NonTerminalSymbol.X));
                            stack.Push(new Symbol(TerminalSymbolType.Variable));
                        }
                        else
                        {
                            var errorMsg = String.Format("Unexpected token '{0}'",
                                Tokens[position].Value);
                            TokenError = new TerminalSymbol(Tokens[position].Type,
                                Tokens[position].Column, errorMsg);
                            SyntaxError = true;
                            break;
                        }
                    }
                }
                else 
                {
                    TerminalSymbolType symbolValue = current.TerminalSymbol;
                    Console.Out.Write("   >> Expecting {0}", symbolValue);
                    if (position < N)
                    {
                        if (Tokens[position] == symbolValue)
                        {
                            Console.Out.WriteLine(" - matched with {0}", Tokens[position]);
                            position++;
                        }
                        else 
                        {
                            var errorMsg = String.Format("Expecting {0}, got {1}",
                                symbolValue, Tokens[position].Value);
                            TokenError = new TerminalSymbol(Tokens[position].Type,
                                Tokens[position].Column, errorMsg);
                            SyntaxError = true;
                            break;
                        }
                    }
                    else
                    {
                        var errorMsg = String.Format("Expecting {0}, got enf of expression",
                                symbolValue);
                        TokenError = new TerminalSymbol(TerminalSymbolType.UnRecognized,
                            N, errorMsg);
                        SyntaxError = true;                        
                        break;
                    }
                    
                }
            }
            if (position < N && !SyntaxError)
            {
                TokenError = new TerminalSymbol(TerminalSymbolType.UnRecognized, position, 
                    "Sintax error");
                SyntaxError = true;
            }
        }
    }
}
