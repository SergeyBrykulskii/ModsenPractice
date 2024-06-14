using Calculator.Models;
using System.Text.RegularExpressions;

namespace Calculator.Services;

public class InputPreprocessingService
{
    /// <summary>
    /// Replaces function calls with their bodies with passed arguments
    /// </summary>
    /// <param name="input">Infix notation string with function calls</param>
    /// <returns>Infix notation string without function calls</returns>
    public string ReplaceUserFunctions(string input, Dictionary<string, UserFunction> functions) {
        foreach (var function in functions) {
            string funcName = function.Key;
            var userFunction = function.Value;
            Regex regex = new Regex($@"\b{funcName}\(([^)]*)\)");
            input = regex.Replace(input, match =>
            {
                var vars = match.Groups[1].Value.Split(',');
                try {
                    if (vars.Length != userFunction.Variables.Count) {
                        throw new ArgumentException($"Function {funcName} expects {userFunction.Variables.Count} arguments, but got {vars.Length}.");
                    }
                    string expression = userFunction.Expression;
                    for (int i = 0; i < userFunction.Variables.Count; i++) {
                        expression = expression.Replace(userFunction.Variables[i], vars[i].Trim());
                    }
                    return $"({expression})";
                }
                catch (ArgumentException e)
                {
                    return e.Message;
                }
            });
        }
        return input;
    }

    /// <summary>
    /// Create user function object from input string
    /// </summary>
    /// <param name="inputFunc"></param>
    /// <returns></returns>
    public UserFunction ProcessFunction(string inputFunc)
    {
        var outputFunc = new UserFunction();
        string pattern = @"^(?<name>\w+)\((?<variables>[\w,]+)\)=(?<expression>.+)$";
        Match match = Regex.Match(inputFunc, pattern);

        if (!InputValidationService.FunctionValidation(inputFunc))
        {
            throw new ArgumentException("Invalid input string format");
        }
        else
        {
            outputFunc.Name = match.Groups["name"].Value;
            outputFunc.Variables = new List<string>(match.Groups["variables"].Value.Split(','));
            outputFunc.Expression = match.Groups["expression"].Value;
            return outputFunc;
        }
    }

	/// <summary>
	/// Create tuple of variable name and value from input string
	/// </summary>
	/// <param name="inputVar"></param>
	/// <returns></returns>
	public (string Name, string Value) ProcessVariable(string inputVar)
	{
        string pattern = @"^(?<name>\w+)=(?<value>.+)$";
        Match match = Regex.Match(inputVar, pattern);

        if (!InputValidationService.VariableValidation(inputVar))
        {
            throw new ArgumentException("Invalid input string format");
        }
        else
        {
            return (match.Groups["name"].Value, match.Groups["value"].Value);
        }
    }
}
