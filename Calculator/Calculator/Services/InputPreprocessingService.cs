using Calculator.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator.Services;

public static class InputPreprocessingService
{
    /// <summary>
    /// Replaces function calls with their bodies with passed arguments
    /// </summary>
    /// <param name="input">Infix notation string with function calls</param>
    /// <returns>Infix notation string without function calls</returns>
    public static string ReplaceUserFunctions(string input, Dictionary<string, UserFunction> functions)
    {
        foreach (var function in functions)
        {
            var funcName = function.Key;
            var userFunction = function.Value;
            var regex = new Regex($@"\b{funcName}\((?<args>(?>[^()]+|(?<open>\()|(?<-open>\)))+(?(open)(?!)))\)");
            input = regex.Replace(input, match => ReplaceFunctionCall(match, userFunction));
        }
        return input;
    }

    private static string ReplaceFunctionCall(Match match, UserFunction userFunction)
    {
        var args = new List<string>();
        var currentArg = new StringBuilder();
        var openParensCount = 0;

        foreach (var c in match.Groups[1].Value)
        {
            if (c == '(')
            {
                openParensCount++;
                currentArg.Append(c);
            }
            else if (c == ')')
            {
                openParensCount--;
                currentArg.Append(c);
            }
            else if (c == ',' && openParensCount == 0)
            {
                args.Add(currentArg.ToString().Trim());
                currentArg.Clear();
            }
            else
            {
                currentArg.Append(c);
            }
        }
        if (currentArg.Length > 0)
        {
            args.Add(currentArg.ToString().Trim());
        }
        try
        {
            if (args.Count != userFunction.Variables.Count)
            {
                throw new ArgumentException($"Function {userFunction.Name} expects {userFunction.Variables.Count} arguments, but got {args.Count}.");
            }
            string expression = userFunction.Expression;
            for (int i = 0; i < userFunction.Variables.Count; i++)
            {
                expression = expression.Replace(userFunction.Variables[i], args[i]);
            }
            return $"({expression})";
        }
        catch (ArgumentException e)
        {
            return e.Message;
        }
    }

    /// <summary>
    /// Create user function object from input string
    /// </summary>
    /// <param name="inputFunc"></param>
    /// <returns></returns>
    public static UserFunction ProcessFunction(string inputFunc, Dictionary<string, UserFunction> functions)
    {
        var outputFunc = new UserFunction();
        var pattern = @"^(?<name>\w+)\((?<variables>[\w,]+)\)=(?<expression>.+)$";
        var match = Regex.Match(inputFunc, pattern);

        if (!InputValidationService.FunctionValidation(inputFunc, functions))
        {
            throw new ArgumentException("Invalid input string format");
        }

        outputFunc.Name = match.Groups["name"].Value;
        outputFunc.Variables = new List<string>(match.Groups["variables"].Value.Split(','));
        outputFunc.Expression = ReplaceUserFunctions(match.Groups["expression"].Value, functions);
        return outputFunc;
    }

    /// <summary>
    /// Replace variables with their value in input string 
    /// </summary>
    /// <param name="input">Infix notation string with user veriables</param>
    /// <param name="userVariables">Dictionary of varibles</param>
    /// <returns></returns>
    public static string ReplaceUserVariables(string input, Dictionary<string, string> userVariables)
    {
        var pattern = "[a-zA-Z]+";
        input = Regex.Replace(input, pattern, match =>
        {
            if (userVariables.TryGetValue(match.Groups[0].Value, out string? value))
            {
                return value;
            }
            else
            {
                return match.Groups[0].Value;
            }
        });

        return input;
    }

    /// <summary>
    /// Create tuple of variable name and value from input string
    /// </summary>
    /// <param name="inputVar"></param>
    /// <returns></returns>
    public static (string Name, string Value) ProcessVariable(string inputVar)
    {
        var pattern = @"^(?<name>\w+)=(?<value>.+)$";
        var match = Regex.Match(inputVar, pattern);

        if (!InputValidationService.VariableValidation(inputVar))
        {
            throw new ArgumentException("Invalid input string format");
        }

        return (match.Groups["name"].Value, match.Groups["value"].Value);
    }
}
