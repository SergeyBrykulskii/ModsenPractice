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

		if (match.Success)
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
}
