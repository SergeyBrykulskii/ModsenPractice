using Calculator.Services;

namespace Calculator.Tests.Services;

public class InputValidationServiceTests
{

	[Fact]
	public void FunctionValidation_Correct()
	{
		string input = "funcName(var1,var2)=var1+var2";
		var result = InputValidationService.FunctionValidation(input);

		Assert.True(result);
	}

	[Fact]
	public void FunctionValidation_InvalidVariableName()
	{
		string input = "funcName(1var1,var2)=var1+var2";
		var result = InputValidationService.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void FunctionValidation_InvalidFunctionName()
	{
		string input = "1funcName(var1,var2)=var1+var2";
		var result = InputValidationService.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void FunctionValidation_InvalidVariableNameInBody()
	{
		string input = "funcName(var1,var2)=var1+3var2";
		var result = InputValidationService.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void FunctionValidation_MissingEqualSign()
	{
		string input = "funcName(var1,var2)var1+var2";
		var result = InputValidationService.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void FunctionValidation_MissingComa()
	{
		string input = "funcName(var1,var2=var1+var2";
		var result = InputValidationService.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void FunctionValidation_UndefinedVariableInBody()
	{
		string input = "funcName(var1)=var1+var2";
		var result = InputValidationService.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void FunctionValidation_UnusedVariableInDefinition()
	{
		string input = "funcName(var1,var2)=var1+5";
		var result = InputValidationService.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void VariableValidation_Correct_PositiveNumber()
	{
		string input = "x=5";
		var result = InputValidationService.VariableValidation(input);

		Assert.True(result);
	}

	[Fact]
	public void VariableValidation_Correct_NegativeNumber()
	{
		string input = "x=-5";
		var result = InputValidationService.VariableValidation(input);

		Assert.True(result);
	}

	[Fact]
	public void VariableValidation_Correct_FractionalNumber()
	{
		string input = "x=5.6";
		var result = InputValidationService.VariableValidation(input);

		Assert.True(result);
	}

	[Fact]
	public void VariableValidation_InvalidVariableName()
	{
		string input = "1x=4";
		var result = InputValidationService.VariableValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void VariableValidation_MissingEqualSign()
	{
		string input = "x4";
		var result = InputValidationService.VariableValidation(input);

		Assert.False(result);
	}
}
