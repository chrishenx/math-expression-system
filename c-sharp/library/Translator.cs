//=======================================================================
//  Translator.cs
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
using System.Collections.Generic;

namespace chrishenx.MathExpressionSystem
{
    using Token = TerminalSymbol;
    using Tokens = List<TerminalSymbol>;
    using Instructions = LinkedList<Operation>;

    public sealed class Translator
    {
        public bool ExpressionValid { get; private set; }
        public Token TokenError { get; private set; }
        public Tokens Tokens { get; private set; }
        private int MaxLevel;
        private List<int> Levels;


        public Translator(string expression)
        {
            var lexer = new Lexer(expression);
            var tokens = lexer.GenerateTokens();
            Console.Out.WriteLine("\nTokens:");
            foreach (var token in tokens)
            {
                Console.Out.WriteLine("  " + token);
            }
            if (!lexer.LexicalError)
            {
                var parser = new Parser(tokens);
                parser.ValidateSintax();
                if (!parser.SyntaxError)
                {
                    ExpressionValid = true;
                    var tuple = AdecuateTokens(tokens);
                    Tokens = tuple.Item1;
                    Levels = tuple.Item2;
                    MaxLevel = tuple.Item3;
                }
                else
                {
                    ExpressionValid = false;
                    TokenError = parser.TokenError;
                }
            }
            else
            {
                ExpressionValid = false;
                TokenError = lexer.TokenError;
            }
        }

        public Instructions GenerateAlgorithm()
        {
            if (!ExpressionValid) return null;

            int N = Tokens.Count;
            var evaluated = CreateArray<bool>(N, false);
            var addresses = CreateArray<int>(N, -1);
            var instructions = new Instructions();
            var instructionsNames = new LinkedList<string>(); // Just for testing

            if (N == 1)
            {
                var operand = new Operand(true, Tokens[0].Value);
                instructions.AddLast(new Operation(operand, 0, Operation.MakeFunction("u+")));
                instructionsNames.AddLast("u+");
            }
            else 
            {
                Console.Out.WriteLine("\nProccess to generate the set of instructions\n");
                int right = 0;
                int left = 0;
                bool rightIsValue = false;
                bool leftIsValue = false;
                int lastAddress = 0;
                for (int currentLevel = MaxLevel; currentLevel >= 0; currentLevel--) 
                {
                    Console.Out.WriteLine("Current level " + currentLevel);

                    // Handling functions
                    for (int i = 0; i < N; i++) 
                    {
                        if (Levels[i] == currentLevel && Tokens[i] == TerminalSymbolType.Function) 
                        {
                            Console.Out.WriteLine("Processing {0} in {1}", Tokens[i].Value, i);
                            for (right = i + 1; right < N; right++)
                                if(!evaluated[right]) break;
                            evaluated[right] = true;
                            TerminalSymbol rightToken = Tokens[right];
                            rightIsValue = addresses[right] == -1;
                            Operand operand = new Operand(rightIsValue,
                                rightIsValue ? rightToken.Value : addresses[right].ToString());
                            instructions.AddLast(new Operation(operand, lastAddress, 
                                Operation.MakeFunction(Tokens[i].Value)));
                            instructionsNames.AddLast(Tokens[i].Value);
                            addresses[i] = lastAddress; 
                            lastAddress++;
                        }
                    }

                    // Handling power operator
                    for (int i = 0; i < N; i++) 
                    {
                        if (Levels[i] == currentLevel && Tokens[i].Value.Equals("^")) 
                        {
                            Console.Out.WriteLine("Processing {0} in {1}", Tokens[i].Value, i);
                            for (left = i - 1; left > 0; left--) 
                                if(!evaluated[left]) break;
                            for (right = i + 1; right < N; right++)
                                if(!evaluated[right]) break;
                            evaluated[i] = true;
                            evaluated[right] = true;
                            TerminalSymbol leftToken = Tokens[left];
                            TerminalSymbol rightToken = Tokens[right];
                            leftIsValue = addresses[left] == -1;
                            rightIsValue = addresses[right] == -1;
                            Operand left_operand= new Operand(leftIsValue, 
                            leftIsValue? leftToken.Value : addresses[left].ToString());
                            Operand right_operand = new Operand(rightIsValue, 
                            rightIsValue ? rightToken.Value : addresses[right].ToString());
                            addresses[left] = lastAddress;
                            instructions.AddLast(new Operation(left_operand, right_operand, lastAddress, 
                                Operation.MakeFunction("^")));
                            instructionsNames.AddLast(Tokens[i].Value);
                            lastAddress++;
                        }
                    }

                    // Handling * and / operators
                    for (int i = 0; i < N; i++) 
                    {
                        if (Levels[i] == currentLevel && "*/".Contains(Tokens[i].Value)) 
                        {
                            Console.Out.WriteLine("Processing {0} in {1}", Tokens[i].Value, i);
                            for (left = i - 1; left > 0; left--) 
                                if(!evaluated[left]) break;
                            for (right = i + 1; right < N; right++)
                                if(!evaluated[right]) break;
                            evaluated[i] = true;
                            evaluated[right] = true;
                            TerminalSymbol leftToken = Tokens[left];
                            TerminalSymbol rightToken = Tokens[right];
                            leftIsValue = addresses[left] == -1;
                            rightIsValue = addresses[right] == -1;
                            Operand left_operand= new Operand(leftIsValue, 
                                leftIsValue? leftToken.Value : addresses[left].ToString());
                            Operand right_operand = new Operand(rightIsValue, 
                                rightIsValue ? rightToken.Value : addresses[right].ToString());
                            addresses[left] = lastAddress;
                            instructions.AddLast(new Operation(left_operand, right_operand, lastAddress, 
                                Operation.MakeFunction(Tokens[i].Value)));
                            instructionsNames.AddLast(Tokens[i].Value);
                            lastAddress++;
                        }
                    }

                    // Handling + and - operators, unary and binary
                    for (int i = 0; i < N; i++) 
                    {
                        if (Levels[i] == currentLevel && "+-".Contains(Tokens[i].Value)) 
                        {
                            Console.Out.WriteLine("Processing {0} in {1}", Tokens[i].Value, i);
                            for (left = i - 1; left > 0; left--) 
                                if(!evaluated[left]) break;
                            for (right = i + 1; right < N; right++)
                                if(!evaluated[right]) break;
                            evaluated[right] = true;
                            TerminalSymbol leftToken = null;
                            TerminalSymbol rightToken = Tokens[right];
                            leftIsValue = false;
                            rightIsValue = addresses[right] == -1;
                            Operand left_operand = null; 
                            Operand right_operand = new Operand(rightIsValue,
                                rightIsValue ? rightToken.Value : addresses[right].ToString());
                            if (left >= 0)
                            {
                                leftToken = Tokens[left];
                                leftIsValue = addresses[left] == -1;
                                left_operand = new Operand(leftIsValue,
                                 leftIsValue ? leftToken.Value : addresses[left].ToString());
                            }
                            if (i == 0 || Levels[i - 1] < currentLevel)
                            {
                                Console.Out.WriteLine(" - Unary");
                                addresses[i] = lastAddress;
                                instructions.AddLast(new Operation(right_operand, lastAddress, 
                                    Operation.MakeFunction("u" + Tokens[i].Value)));
                                instructionsNames.AddLast("u" + Tokens[i].Value);
                            }
                            else
                            {
                                Console.Out.WriteLine(" - Binary");
                                evaluated[i] = true;
                                addresses[left] = lastAddress;
                                instructions.AddLast(new Operation(left_operand, right_operand, 
                                    lastAddress, Operation.MakeFunction("b" + Tokens[i].Value)));
                                instructionsNames.AddLast("b" + Tokens[i].Value);
                            }
                            lastAddress++;
                        }
                    }
                }
            }
            Console.Out.WriteLine("\nInstructions: \n");
            var it1 = instructions.First;
            var it2 = instructionsNames.First;
            while (it1 != null) {
                Console.Out.WriteLine("{0} : {1}", it2.Value, it1.Value);
                it1 = it1.Next;
                it2 = it2.Next;
            }
            Console.Out.WriteLine();
            return instructions;
        }

        // This method computes the depth of each tokens
        // and returns the tokens without parenthesis
        private Tuple<Tokens, List<int>, int> AdecuateTokens(Tokens tokens)
        {
            int N = tokens.Count;
            var Tokens = new Tokens(N);
            var Levels = new List<int>(N);
            int maxLevel = 0;
            for (int i = 0, level = 0; i < N; i++)
            {
                if (tokens[i] == TerminalSymbolType.OpeningParenthesis)
                {
                    level++;
                    if (level > maxLevel) maxLevel = level;
                }
                else if (tokens[i] == TerminalSymbolType.ClosingParenthesis)
                {
                    level--;
                }
                else
                {
                    Levels.Add(level);
                    Tokens.Add(tokens[i]);
                }
            }
            return Tuple.Create(Tokens, Levels, maxLevel);
        }

        private static T[] CreateArray<T>(int N, T initialValue)
        {
            var array = new T[N];
            for (int i = 0; i < N; i++)
            {
                array[i] = initialValue;
            }
            return array;
        }
    }
}
