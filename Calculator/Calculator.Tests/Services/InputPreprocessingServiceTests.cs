namespace Calculator.Tests.Services;



using System.Collections.Generic;
using Calculator.Models;
using Calculator.Services;


public class InputPreprocessingServiceTests
{
    private InputPreprocessingService service = new InputPreprocessingService();
    [Fact]
    public void ReplaceUserFunctions_ValidInput_ReplacesFunctionCalls()
    {
        var functions = new Dictionary<string, UserFunction> {
            { "f", new UserFunction { Name = "f", Variables = new List<string> { "x", "y" }, Expression = "x + y" } }
        };
        string input = "3 + f(1, 2)";
        string result = service.ReplaceUserFunctions(input, functions);
        Assert.Equal("3 + (1 + 2)", result);
    }

    [Fact]
    public void ReplaceUserFunctions_MismatchedArgumentCount_ThrowsArgumentException()
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
}
