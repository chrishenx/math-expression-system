using System;
using System.IO;

namespace MathExpressionSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Out.Write("Introduce expression: ");
                var expression = Console.In.ReadLine();
                var translator = new Translator(expression);
                if (translator.ExpressionValid)
                {
                    Console.Out.WriteLine("\nExpression valid");
                    var instructions = translator.GenerateAlgorithm();
                    var evaluator = new Evaluator(instructions, translator.Tokens);
                    evaluator.Variables.Add('x', 2);
                    try
                    {
                        Console.Out.WriteLine("\n\nRESULT: {0}\n", evaluator.Evaluate());
                    }
                    catch (Exception e)
                    {
                        Console.Out.WriteLine("\n\n Evaluation error: {0}\n", e.Message);
                    }
                }
                else
                {
                    var tokenError = translator.TokenError;
                    Console.Out.WriteLine("\nError at {0}: {1}", tokenError.Column, tokenError.Value);
                }
                Console.Out.WriteLine("Press ENTER to continue...");
                Console.In.ReadLine();
                Console.Clear();
                Console.Out.Flush();
            }
             /*
            while (true)
            {
                Console.Out.Write("Introduce expression: ");
                var expression = Console.In.ReadLine();
                var lexer = new Lexer(expression);
                var tokens = lexer.GenerateTokens();
                Console.Out.WriteLine("Expression {0}\n", expression);
                foreach (var token in tokens)
                {
                    Console.Out.WriteLine("  " + token);
                }          
                if (lexer.LexicalError)
                {
                    var tokenError = lexer.TokenError;
                    Console.Out.WriteLine("\nError at {0}: {1}", tokenError.Column, tokenError.Value);
                }
                else
                {
                    Console.Out.WriteLine("\nTokens generated without problems!");
                    var parser = new Parser(tokens);
                    parser.ValidateSintax();
                    if (parser.SyntaxError)
                    {
                        var tokenError = parser.TokenError;
                        Console.Out.WriteLine("\nError at {0}: {1}", tokenError.Column, tokenError.Value);
                    }
                    else
                    {
                        Console.Out.WriteLine("\nCorrect sintax!");
                    }
                }
                Console.Out.WriteLine("Press ENTER to continue...");
                Console.In.ReadLine();
                Console.Clear();
                Console.Out.Flush();
            }
              */
        }
    }
}
