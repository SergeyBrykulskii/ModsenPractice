using System.Text.RegularExpressions;
using Calculator.Models;

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
        try {
            var parts = inputFunc.Split('=');
            if (parts.Length != 2) {
                throw new ArgumentException("Invalid function format. Expected format: f(x, y) = x + y");
            }
            string signature = parts[0].Trim();
            string expression = parts[1].Trim();
            Match match = Regex.Match(signature, @"(\w+)\(([^)]*)\)");
            if (!match.Success) {
                throw new ArgumentException("Invalid function signature. Expected format: f(x, y)");
            }
            string name = match.Groups[1].Value;
            var variables = match.Groups[2].Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return new UserFunction {
                Name = name,
                Variables = new List<string>(variables.Select(v => v.Trim())),
                Expression = expression
            };
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
