using Calculator.Services;

namespace Calculator.Tests.Services;



using System.Collections.Generic;
using Calculator.Models;
using Calculator.Services;


public class InputPreprocessingServiceTests
{
    private InputPreprocessingService service = new InputPreprocessingService();
    [Fact]
    public void ReplaceUserFunctions_ValidInput_ReplacesUserFunctionCalls()
    {
        var functions = new Dictionary<string, UserFunction> {
            { "f", new UserFunction { Name = "f", Variables = new List<string> { "x", "y" }, Expression = "x + y" } }
        };
        string input = "3 + f(1, 2)";
        
        string result = service.ReplaceUserFunctions(input, functions);
        
        Assert.Equal("3 + (1 + 2)", result);
    }

    [Fact]
    public void ReplaceUserFunctions_MismatchedArgumentCount_ReturnsExceptionMessage()
    {
        var functions = new Dictionary<string, UserFunction>
        {
            { "f", new UserFunction { Name = "f", Variables = new List<string> { "x", "y" }, Expression = "x + y" } }
        };
        string input = "3 + f(1)";
        
        string result = service.ReplaceUserFunctions(input, functions);
        
        Assert.True(result.Contains("Function f expects 2 arguments, but got 1."));
    }

    [Fact]
    public void ReplaceUserFunctions_NoFunctionCall_ReturnsOriginalInput()
    {
        var functions = new Dictionary<string, UserFunction>();
        string input = "3 + 5";
        
        string result = service.ReplaceUserFunctions(input, functions);
        
        Assert.Equal("3 + 5", result);
    }
    
    [Fact]
    public void ReplaceUserFunctions_FunctionAsParam_ReplacesUserFunctionCalls()
    {
	    var functions = new Dictionary<string, UserFunction> {
		    { "f", new UserFunction { Name = "f", Variables = new List<string> { "x", "y" }, Expression = "x + y" } },
		    { "f1", new UserFunction { Name = "f1", Variables = new List<string> { "a", "b" }, Expression = "a * b" } }
	    };
	    string input = "3 + f(f1(1, 2), 2)";
	    
	    string result = service.ReplaceUserFunctions(input, functions);
	    
	    Assert.Equal("3 + ((1 * 2) + 2)", result);
    }
	
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
