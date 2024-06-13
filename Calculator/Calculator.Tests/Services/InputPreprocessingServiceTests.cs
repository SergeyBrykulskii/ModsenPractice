using Calculator.Services;

namespace Calculator.Tests.Services;

public class InputPreprocessingServiceTests
{

	[Fact]
	public void ProcessFunction_Correct()
	{
		var processor = new InputPreprocessingService();
		string input = "funcName(var1,var2)=var1+var2";
		var result = processor.ProcessFunction(input);

		Assert.Equal("funcName", result.Name);
		Assert.Equal(new List<string> { "var1", "var2" }, result.Variables);
		Assert.Equal("var1+var2", result.Expression);
	}

	[Fact]
	public void ProcessFunction_InvalidInput()
	{
		var processor = new InputPreprocessingService();
		string input = "funcName(var1,var2)var1+var2";

		Assert.Throws<ArgumentException>(() => processor.ProcessFunction(input));
	}

	[Fact]
	public void FunctionValidation_Correct()
	{
		var validator = new InputPreprocessingService();
		string input = "funcName(var1,var2)=var1+var2";
		var result = validator.FunctionValidation(input);

		Assert.True(result);
	}

	[Fact]
	public void FunctionValidation_InvalidVariableName()
	{
		var validator = new InputPreprocessingService();
		string input = "funcName(1var1,var2)=var1+var2";
		var result = validator.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void FunctionValidation_InvalidFunctionName()
	{
		var validator = new InputPreprocessingService();
		string input = "1funcName(var1,var2)=var1+var2";
		var result = validator.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void FunctionValidation_InvalidVariableNameInBody()
	{
		var validator = new InputPreprocessingService();
		string input = "funcName(var1,var2)=var1+3var2";
		var result = validator.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void FunctionValidation_MissingEqualSign()
	{
		var validator = new InputPreprocessingService();
		string input = "funcName(var1,var2)var1+var2";
		var result = validator.FunctionValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void FunctionValidation_MissingComa()
	{
		var validator = new InputPreprocessingService();
		string input = "funcName(var1,var2=var1+3var2";
		var result = validator.FunctionValidation(input);

		Assert.False(result);
	}
	[Fact]
	public void ProcessVariable_Correct()
	{
		var processor = new InputPreprocessingService();
		string input = "x=5";
		var result = processor.ProcessVariable(input);

		Assert.Equal("x", result.Name);
		Assert.Equal("5", result.Value);
	}

	[Fact]
	public void ProcessVariable_InvalidInput()
	{
		var processor = new InputPreprocessingService();
		string input = "x";

		Assert.Throws<ArgumentException>(() => processor.ProcessVariable(input));
	}

	[Fact]
	public void VariableValidation_Correct_PositiveNumber()
	{
		var validator = new InputPreprocessingService();
		string input = "x=5";
		var result = validator.VariableValidation(input);

		Assert.True(result);
	}

	[Fact]
	public void VariableValidation_Correct_NegativeNumber()
	{
		var validator = new InputPreprocessingService();
		string input = "x=-5";
		var result = validator.VariableValidation(input);

		Assert.True(result);
	}

	[Fact]
	public void VariableValidation_Correct_FractionalNumber()
	{
		var validator = new InputPreprocessingService();
		string input = "x=5.6";
		var result = validator.VariableValidation(input);

		Assert.True(result);
	}

	[Fact]
	public void VariableValidation_InvalidVariableName()
	{
		var validator = new InputPreprocessingService();
		string input = "1x=4";
		var result = validator.VariableValidation(input);

		Assert.False(result);
	}

	[Fact]
	public void VariableValidation_MissingEqualSign()
	{
		var validator = new InputPreprocessingService();
		string input = "x4";
		var result = validator.VariableValidation(input);

		Assert.False(result);
	}
}
