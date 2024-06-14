using System.Globalization;

namespace Calculator.Services;

/// <summary>
///  Reverse Polish notation processing service
/// </summary>

public static class CharExtensions
{
    public static bool IsOperator(this char symbol)
    {
        return "()+-*/".Contains(symbol);
    }
}
public class RpnService
{
    public string InfixNotationToRpn(string input)
    {
        var operatorsPriority = new Dictionary<char, int>()
         {
            { '(', 0},
            { '+', 1},
            { '-', 1},
            { '*', 2},
            { '/', 2}
         };

        Stack<char> operators = new();
        string output = string.Empty;

        for (int i = 0; i < input.Length; i++)
        {
            if (Char.IsDigit(input[i])) //read the number
            {
                while (!input[i].IsOperator())
                {
                    output += input[i];
                    i++;

                    if (i == input.Length) break;
                }
                output += " ";
                i--;
            }

            if (input[i].IsOperator())
            {
                if (input[i] == '(')
                {
                    operators.Push(input[i]);
                }
                else if (input[i] == ')') //push all operators up to the opening bracket from the stack, and remove the opening bracket from the stack
                {
                    while (operators.Peek() != '(')
                        output += operators.Pop().ToString() + " ";
                    operators.Pop();
                }
                else if (input[i] == '-' && (i == 0 || (i >= 1 && operatorsPriority.ContainsKey(input[i - 1])))) //for unary minus
                {
                    output += "0 ";
                    operators.Push(input[i]);
                }
                else
                {
                    if (operators.Count > 0)
                    {
                        while (operators.Count > 0 && operatorsPriority[operators.Peek()] >= operatorsPriority[input[i]]) //push into the output from the stack all operators that have a priority higher than the current
                            output += operators.Pop().ToString() + " ";
                    }
                    operators.Push(input[i]);
                }
            }
        }
        output += string.Join(" ", operators.Select(op => op.ToString()));
        return output;
    }

    public double СalculateRpn(string inputRPN)
    {
        var temp = new Stack<double>();

        for (int i = 0; i < inputRPN.Length; i++)
        {

            if (Char.IsDigit(inputRPN[i]))
            {
                string a = string.Empty;

                while (!inputRPN[i].IsOperator() && inputRPN[i] != ' ')
                {
                    a += inputRPN[i];
                    i++;
                    if (i == inputRPN.Length) break;
                }

                temp.Push(double.Parse(a, CultureInfo.InvariantCulture));
                i--;
            }
            else if (inputRPN[i].IsOperator())
            {

                double a = temp.Pop();
                double b = temp.Pop();

                double result = inputRPN[i] switch
                {
                    '+' => b + a,
                    '-' => b - a,
                    '*' => b * a,
                    '/' => b / a,
                    _ => throw new ArgumentException("Invalid operator"),
                };
                temp.Push(result);
            }
        }
        return temp.Peek();
    }
}
