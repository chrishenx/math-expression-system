using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chrishenx.MathExpressionSystem
{
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

    using Token = TerminalSymbol;
    using Tokens = List<TerminalSymbol>;
    using TokenType = TerminalSymbolType;

    public sealed class Lexer
    {
        public string Expression { get; set; }
        public Token TokenError { get; private set; }
        public bool LexicalError { get; private set; }
        private static readonly char EndOfExpression = ' ';
        public static readonly string[] Functions;

        static Lexer()
        {
            Functions = new string[] { 
                "sin", "cos", "tan", "arcsin", "arccos", "arctan", // Trigonometric
                "sec", "cosec", "cotan",
                "sinh", "cosh", "tanh", // Hyperbolic
                "exp", "ln", "log", "sqrt", "abs", "cbrt", "sinc", "hstep" // Miscellaneous
            }; 
        }

        public Lexer(string expression)
        {
            Expression = expression;
            LexicalError = false;
        }

        private static bool IsFunction(string str)
        {
            return Array.IndexOf<string>(Functions, str) != -1;
        }

        private Token makeOneCharToken(string str, int column)
        {
            char symbol = str[0];
            if ("+-*/^".Contains(symbol))
            {
                return new Token(TokenType.Operator, column, str);
            }
            else if (symbol == '(')
            {
                return new Token(TokenType.OpeningParenthesis, column, str);
            }
            else if (symbol == ')')
            {
                return new Token(TokenType.ClosingParenthesis, column, str);
            }
            return null;
        }

        public Tokens GenerateTokens()
        {
            int N = Expression.Length;
            Tokens tokens = new Tokens(N);
            StringBuilder tokenValue = new StringBuilder(N);
            State currentState = State.BEGIN;
            int position = 0;
            while (position <= N && !LexicalError)
            {
                char symbol = position < N ? Expression[position] : EndOfExpression;
                switch (currentState)
                {
                    case State.BEGIN:
                        if (Char.IsLetter(symbol))
                        {
                            tokenValue.Append(symbol);
                            currentState = State.VAR;
                        }
                        else if (Char.IsDigit(symbol))
                        {
                            tokenValue.Append(symbol);
                            currentState = State.NUM1;
                        }
                        else if ("+-*/^()".Contains(symbol))
                        {
                            tokenValue.Append(symbol);
                            currentState = State.OK;
                        }
                        else if (symbol != EndOfExpression)
                        {
                            if (position < N)
                            {
                                var error = String.Format("Invalid symbol {0}", symbol);
                                TokenError = new Token(TerminalSymbolType.UnRecognized, position, error);
                                tokens.Add(TokenError);
                                LexicalError = true;
                            }
                        }
                        position++;
                        break;
                    case State.VAR:
                        if (Char.IsLetter(symbol))
                        {
                            currentState = State.FUNC;
                            tokenValue.Append(symbol);
                            position++;
                        }
                        else
                        {
                            tokens.Add(new Token(TokenType.Variable, position, tokenValue.ToString()));
                            tokenValue.Clear();
                            currentState = State.BEGIN;
                        }
                        break;
                    case State.FUNC:
                        if (Char.IsLetter(symbol))
                        {
                            tokenValue.Append(symbol);
                            position++;
                        } 
                        else
                        {
                            var str = tokenValue.ToString(); 
                            if (IsFunction(str))
                            {
                                tokens.Add(new Token(TokenType.Function, position, str));
                                tokenValue.Clear();
                                currentState = State.BEGIN;
                            }
                            else
                            {
                                var error = String.Format("Unrecognized {0}", str);
                                TokenError = new Token(TokenType.UnRecognized, position, error);
                                tokens.Add(TokenError);
                                LexicalError = true;
                            }
                        }
                        break;
                    case State.OK:
                        tokens.Add(makeOneCharToken(tokenValue.ToString(), position));
                        tokenValue.Clear();
                        currentState = State.BEGIN;
                        break;
                    case State.NUM1:
                        if (Char.IsDigit(symbol))
                        {
                            tokenValue.Append(symbol);
                            position++;
                        }
                        else if (symbol == '.')
                        {
                            tokenValue.Append(symbol);
                            position++;
                            currentState = State.TRANS;
                        }
                        else
                        {
                            tokens.Add(new Token(TokenType.Value, position, tokenValue.ToString()));
                            tokenValue.Clear();
                            currentState = State.BEGIN;
                        }
                        break;
                    case State.TRANS:
                        if (Char.IsDigit(symbol))
                        {
                            tokenValue.Append(symbol);
                            position++;
                            currentState = State.NUM2;
                        }
                        else
                        {
                            var error = String.Format("Invalid number format {0}", tokenValue.Append(symbol));
                            TokenError = new Token(TokenType.UnRecognized, position, error);
                            tokens.Add(TokenError);
                            LexicalError = true;
                        }
                        break;
                    case State.NUM2:
                        if (Char.IsDigit(symbol))
                        {
                            tokenValue.Append(symbol);
                            position++;
                        }
                        else
                        {
                            tokens.Add(new Token(TokenType.Value, position, tokenValue.ToString()));
                            tokenValue.Clear();
                            currentState = State.BEGIN;
                        }
                        break;
                }

            }
            return tokens;
        }

        private enum State
        {
            BEGIN, VAR, FUNC, OK, NUM1, TRANS, NUM2
        }

    }
}
