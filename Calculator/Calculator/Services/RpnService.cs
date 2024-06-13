namespace Calculator.Services;

/// <summary>
///  Reverse Polish notation processing service
/// </summary>
public class RpnService
{
    static private bool IsOperator(char symbol)
    {
        if ("()+-*/".Contains(symbol))
            return true;
        return false;
    }
    public string InfixNotationToRpn(string input)
    {
         Dictionary<char, int> operatorsPriority = new Dictionary<char, int>()
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
            if (Char.IsDigit(input[i]))
            {
                while (!IsOperator(input[i]))
                {
                    output += input[i];
                    i++;

                    if (i == input.Length) break;
                }
                output += " ";
                i--;
            }

            if (IsOperator(input[i]))
            {
                if (input[i] == '(')
                    operators.Push(input[i]);
                else if (input[i] == ')')
                {
                    while (operators.Peek() != '(')
                        output += operators.Pop().ToString() + " ";
                    operators.Pop();
                }
                else if (input[i] == '-' && (i == 0 || (i > 1 && operatorsPriority.ContainsKey(input[i - 1]))))
                {
                    output += "0 ";
                    operators.Push(input[i]);
                }
                else
                {
                    if (operators.Count > 0)
                    {
                        while (operators.Count > 0 && operatorsPriority[operators.Peek()] >= operatorsPriority[input[i]])
                            output += operators.Pop().ToString() + " ";
                    }
                    operators.Push(input[i]);
                }
            }
        }
        while (operators.Count > 0)
            output += operators.Pop().ToString() + " ";
        return output;
    }

    public double СalculateRpn(string inputRPN)
    {
        double result = 0;
        Stack<double> temp = new Stack<double>(); 

        for (int i = 0; i < inputRPN.Length; i++)
        {
            
            if (Char.IsDigit(inputRPN[i]))
            {
                string a = string.Empty;
                
                while (!IsOperator(inputRPN[i]) && inputRPN[i] != ' ') 
                {
                    a += inputRPN[i]; 
                    i++;
                    if (i == inputRPN.Length) break;
                }
                temp.Push(double.Parse(a)); 
                i--;
            }
            else if (IsOperator(inputRPN[i]))
            {
                
                double a = temp.Pop();
                double b = temp.Pop();

                switch (inputRPN[i]) 
                {
                    case '+': result = b + a; break;
                    case '-': result = b - a; break;
                    case '*': result = b * a; break;
                    case '/': result = b / a; break;
                }
                temp.Push(result); 
            }
        }
        return temp.Peek(); 
    }
}
