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
    public string ReplaceUserFunctions(string input)
    {
        throw new NotImplementedException();
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

        if (FunctionValidation(inputFunc))
        {
            outputFunc.Name = match.Groups["name"].Value;
            outputFunc.Variables = new List<string>(match.Groups["variables"].Value.Split(','));
            outputFunc.Expression = match.Groups["expression"].Value;
            return outputFunc;
        }
        else
        {
            throw new ArgumentException("Invalid input string format");
        }
    }

	/// <summary>
	/// Check if input string is a valid function notation
	/// </summary>
	/// <param name="inputFunc"></param>
	/// <returns>True for valid function</returns>
    public bool FunctionValidation(string inputFunc) 
    {
        string pattern = @"^(?<name>\w+)\((?<variables>[\w,]+)\)=(?<expression>.+)$";
        Match match = Regex.Match(inputFunc, pattern);

        if (match.Success)
        {
            if (char.IsDigit(match.Groups["name"].Value[0]))
            {
                return false;
            }
            var variables = new List<string>(match.Groups["variables"].Value.Split(','));
            if (variables.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (var variable in variables)
                {
                    if (char.IsDigit(variable[0]))
                    {
                        return false;
                    }
                }
            }

            MatchCollection matches = Regex.Matches(match.Groups["expression"].Value, @"\b\w+\b");
            foreach (Match m in matches)
            {
                string variable = m.Value;
                if (!int.TryParse(variable, out _))
                {
                    if (char.IsDigit(variable[0]) || !variables.Contains(variable)) 
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
