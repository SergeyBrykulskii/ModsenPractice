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
}
