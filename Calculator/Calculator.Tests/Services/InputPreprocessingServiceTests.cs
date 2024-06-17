using Calculator.Models;
using Calculator.Services;

namespace Calculator.Tests.Services;

public class InputPreprocessingServiceTests
{
    [Fact]
    public void ReplaceUserFunctions_ValidInput_ReplacesUserFunctionCalls()
    {
        var functions = new Dictionary<string, UserFunction> {
            { "f", new UserFunction { Name = "f", Variables = ["x", "y"], Expression = "x + y" } }
        };
        var input = "3 + f(1, 2)";

        var result = InputPreprocessingService.ReplaceUserFunctions(input, functions);

        Assert.Equal("3 + (1 + 2)", result);
    }

    [Fact]
    public void ReplaceUserFunctions_MismatchedArgumentCount_ReturnsExceptionMessage()
    {
        var functions = new Dictionary<string, UserFunction>
        {
            { "f", new UserFunction { Name = "f", Variables = ["x", "y"], Expression = "x + y" } }
        };
        var input = "3 + f(1)";

        var result = InputPreprocessingService.ReplaceUserFunctions(input, functions);

        Assert.Contains("Function f expects 2 arguments, but got 1.", result);
    }

    [Fact]
    public void ReplaceUserFunctions_NoFunctionCall_ReturnsOriginalInput()
    {
        var functions = new Dictionary<string, UserFunction>();
        var input = "3 + 5";

        var result = InputPreprocessingService.ReplaceUserFunctions(input, functions);

        Assert.Equal("3 + 5", result);
    }

    [Fact]
    public void ReplaceUserFunctions_FunctionAsParam_ReplacesUserFunctionCalls()
    {
        var functions = new Dictionary<string, UserFunction> {
            { "f", new UserFunction { Name = "f", Variables = ["x", "y"], Expression = "x + y" } },
            { "f1", new UserFunction { Name = "f1", Variables = ["x", "y"], Expression = "x * y" } }
        };
        var input = "3 + f(f1(1, 2), 2)";

        var result = InputPreprocessingService.ReplaceUserFunctions(input, functions);

        Assert.Equal("3 + ((1 * 2) + 2)", result);
    }

    [Fact]
    public void ProcessFunction_ValidInput_ReturnsProcessedFunction()
    {
        var functions = new Dictionary<string, UserFunction>();
        var input = "funcName(var1,var2)=var1+var2";
        var result = InputPreprocessingService.ProcessFunction(input, functions);

        Assert.Equal("funcName", result.Name);
        Assert.Equal(["var1", "var2"], result.Variables);
        Assert.Equal("var1+var2", result.Expression);
    }

    [Fact]
    public void ProcessFunction_NestedFunction_ReturnsProcessedFunction()
    {
        var functions = new Dictionary<string, UserFunction>();
        var func = new UserFunction
        {
            Name = "f",
            Variables = ["x", "y"],
            Expression = "x+y"
        };
        functions[func.Name] = func;
        var input = "funcName(var1,var2)=var1+var2+f(1,2)";
        var result = InputPreprocessingService.ProcessFunction(input, functions);

        Assert.Equal("funcName", result.Name);
        Assert.Equal(["var1", "var2"], result.Variables);
        Assert.Equal("var1+var2+(1+2)", result.Expression);
    }

    [Fact]
    public void ProcessFunction_InvalidInput_ThrowsException()
    {
        var functions = new Dictionary<string, UserFunction>();
        var input = "funcName(var1,var2)var1+var2";

        Assert.Throws<ArgumentException>(() => InputPreprocessingService.ProcessFunction(input, functions));
    }

    [Fact]
    public void ProcessVariable_ValidInput_ReturnsProcessedVariable()
    {
        var input = "x=5";
        var result = InputPreprocessingService.ProcessVariable(input);

        Assert.Equal("x", result.Name);
        Assert.Equal("5", result.Value);
    }

    [Fact]
    public void ProcessVariable_InvalidInput_ThrowsException()
    {
        var input = "x";

        Assert.Throws<ArgumentException>(() => InputPreprocessingService.ProcessVariable(input));
    }

    [Fact]
    public void ReplaceUserVariables_NoVariables__ReturnsOriginalInput()
    {
        var input = "3 + 5";
        var variables = new Dictionary<string, string> { { "x", "2" } };

        var result = InputPreprocessingService.ReplaceUserVariables(input, variables);

        Assert.Equal(input, result);
    }

    [Fact]
    public void ReplaceUserVariables_ValidInput_VeriablesReplaced()
    {
        var input = "3 + 5 + x";
        var variables = new Dictionary<string, string> { { "x", "2" } };

        var result = InputPreprocessingService.ReplaceUserVariables(input, variables);

        Assert.Equal("3 + 5 + 2", result);
    }

    [Fact]
    public void ReplaceUserVariables_InvalidVariable_ReturnsOriginalInput()
    {
        var input = "3 + 5 + y";
        var variables = new Dictionary<string, string> { { "x", "2" } };

        var result = InputPreprocessingService.ReplaceUserVariables(input, variables);

        Assert.Equal(input, result);
    }
}
