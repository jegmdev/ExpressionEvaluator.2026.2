using System;
using System.Collections.Generic;
using System.Globalization;

namespace Backend;

public static class ExpressionEvaluator
{
    public static double Evalute(string infix) => EvalutePostfix(ToPostfix(infix));

    private static string ToPostfix(string infix)
    {
        var postfix = new List<string>();
        var stack = new Stack<char>();
        
        for (int i = 0; i < infix.Length; i++)
        {
            char item = infix[i];

            // Si es un dígito o un punto decimal, extraemos el número completo (multidígito y decimales)
            if (char.IsDigit(item) || item == '.')
            {
                string numero = "";
                while (i < infix.Length && (char.IsDigit(infix[i]) || infix[i] == '.'))
                {
                    numero += infix[i];
                    i++;
                }
                i--; // Retroceder un paso para no saltar el siguiente carácter
                postfix.Add(numero);
            }
            else if (IsOperator(item))
            {
                if (item == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        postfix.Add(stack.Pop().ToString());
                    }
                    if (stack.Count > 0 && stack.Peek() == '(')
                    {
                        stack.Pop(); // Eliminar el paréntesis de apertura
                    }
                }
                else
                {
                    if (stack.Count == 0 || item == '(')
                    {
                        stack.Push(item);
                    }
                    else
                    {
                        while (stack.Count > 0 && PriorityInfix(item) <= PriorityStack(stack.Peek()))
                        {
                            postfix.Add(stack.Pop().ToString());
                        }
                        stack.Push(item);
                    }
                }
            }
        }

        while (stack.Count > 0)
        {
            postfix.Add(stack.Pop().ToString());
        }

        return string.Join(" ", postfix);
    }

    private static int PriorityStack(char op) => op switch
    {
        '^' => 3,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 0,
        _ => 0,
    };

    private static int PriorityInfix(char op) => op switch
    {
        '^' => 4,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 5,
        _ => 0,
    };

    private static bool IsOperator(char item) => item == '^' || item == '*' || item == '/' || item == '+' || item == '-' || item == '(' || item == ')';

    private static double EvalutePostfix(string postfixStr)
    {
        var stack = new Stack<double>();
        string[] tokens = postfixStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double numero))
            {
                stack.Push(numero);
            }
            else if (token.Length == 1 && IsOperator(token[0]))
            {
                var ope2 = stack.Pop();
                var ope1 = stack.Pop();
                stack.Push(Calculate(ope1, ope2, token[0]));
            }
        }
        return stack.Pop();
    }

    private static double Calculate(double ope1, double ope2, char item) => item switch
    {
        '*' => ope1 * ope2,
        '/' => ope2 == 0 ? throw new DivideByZeroException() : ope1 / ope2,
        '+' => ope1 + ope2,
        '-' => ope1 - ope2,
        '^' => Math.Pow(ope1, ope2),
        _ => throw new Exception("Invalid operator."),
    };
}