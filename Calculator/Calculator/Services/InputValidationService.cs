using Calculator.Models;
using System.Text.RegularExpressions;

namespace Calculator.Services;

public class InputValidationService
{
	/// <summary>
	/// Check if input string is a valid function notation
	/// </summary>
	/// <param name="inputFunc"></param>
	/// <returns>True for valid function</returns>
	public static bool FunctionValidation(string inputFunc)
	{
		string pattern = @"^(?<name>[A-Za-z]\w*)\((?<variables>[\w,]+)\)=(?<expression>.+)$";
		Match match = Regex.Match(inputFunc, pattern);

		if (!match.Success)
		{
			return false;
		}

		var variables = new List<string>(match.Groups["variables"].Value.Split(','));
		if (variables.Count == 0)
		{
			return false;
		}
		foreach (var variable in variables)
		{
			if (variable.Length == 0 || char.IsDigit(variable[0]))
			{
				return false;
			}
		}


		List<string> funcBodyVariables = new List<string>();
		var expr = match.Groups["expression"].Value;
		MatchCollection matches = Regex.Matches(expr, @"\b\w+\b");

		foreach (Match m in matches)
		{
			string variable = m.Value;
			if (!int.TryParse(variable, out _))
			{
				if (char.IsDigit(variable[0]) || !variables.Contains(variable))
				{
					return false;
				}
				funcBodyVariables.Add(variable);
			}
		}

		foreach (var v in variables)
		{
			if (!funcBodyVariables.Contains(v))
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Check if input string is a valid variable definition
	/// </summary>
	/// <param name="inputVar"></param>
	/// <returns>True for valid variable definition</returns>
	public static bool VariableValidation(string inputVar)
	{
		string pattern = @"^(?<name>[A-Za-z]\w*)=(?<value>-?\d+(?:[.,]\d+)?)$";
		Match match = Regex.Match(inputVar, pattern);

		return match.Success;
	}
}
