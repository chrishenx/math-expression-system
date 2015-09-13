using System;
using System.Collections.Generic;

namespace chrishenx.MathExpressionSystem
{
    using TokenType = TerminalSymbolType;
    using Tokens = List<TerminalSymbol>;
    using Instructions = LinkedList<Operation>;

    class Evaluator
    {
        private double[] Memory;

        public Instructions Instructions { get; set; }
        public bool IsConstantExpression { get; private set; }
        public double LastEvaluation { get; private set; }
        public Dictionary<char, double> Variables  { get; private set; }

        public Evaluator(Instructions instructions, Tokens tokens = null)
        {
            Memory = new double[instructions.Count];
            Variables = new Dictionary<char, double>(30);
            Variables.Add('p', Math.PI);
            Variables.Add('e', Math.E);
            Instructions = instructions;
            IsConstantExpression = true;
            if (tokens == null)
            {
                foreach (var token in tokens) {
                    if (token == TokenType.Variable)
                    {
                        IsConstantExpression = false;
                        break;
                    }
                }
                if (IsConstantExpression)
                {
                    IsConstantExpression = false;
                    Evaluate();
                    IsConstantExpression = true;
                }
            }
            else
            {
                IsConstantExpression = false;
            }
        }

        public double Evaluate()
        {
            if (!IsConstantExpression)
            {
                foreach (var operation in Instructions) 
                {
                    var leftOperand = operation.Left;
                    var rightOperand = operation.Right;
                    double leftValue = 0;
                    double rightValue = 0;
                    if (Char.IsLetter(leftOperand.Value[0]))
                    {
                        leftValue = GetVariableValue(leftOperand.Value[0]);
                    }
                    else
                    {
                        leftValue = Double.Parse(leftOperand.Value);
                        if (!leftOperand.IsValue)
                        {
                            leftValue = Memory[(int)leftValue];

                        }
                    }
                    if (rightOperand != null)
                    {
                        if (Char.IsLetter(rightOperand.Value[0]))
                        {
                            rightValue = GetVariableValue(rightOperand.Value[0]);
                        }
                        else
                        {
                            rightValue = Double.Parse(rightOperand.Value);
                            if (!rightOperand.IsValue)
                            {
                                rightValue = Memory[(int)rightValue];

                            }
                        }
                    }
                    int resultAddress = operation.ResultAddress;
                    Memory[resultAddress] = operation.Function(leftValue, rightValue);
                }
                LastEvaluation = Memory[Instructions.Count - 1];
            }
            return LastEvaluation;
        }

        private double GetVariableValue(char variable)
        {
            if (Variables.ContainsKey(variable))
            {
                return Variables[variable];
            }
            return 0;
        }



    }
}
