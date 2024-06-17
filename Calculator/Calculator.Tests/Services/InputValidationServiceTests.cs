using Calculator.Models;
using Calculator.Services;

namespace Calculator.Tests.Services;

public class InputValidationServiceTests
{

    [Fact]
    public void FunctionValidation_ValidInput_ReturnsTrue()
    {
        var input = "funcName(var1,var2)=var1+var2";
        var functions = new Dictionary<string, UserFunction>();
        var result = InputValidationService.FunctionValidation(input, functions);

        Assert.True(result);
    }

    [Fact]
    public void FunctionValidation_InvalidVariableName_ReturnsFalse()
    {
        var input = "funcName(1var1,var2)=var1+var2";
        var functions = new Dictionary<string, UserFunction>();
        var result = InputValidationService.FunctionValidation(input, functions);

        Assert.False(result);
    }

    [Fact]
    public void FunctionValidation_InvalidFunctionName_ReturnsFalse()
    {
        var input = "1funcName(var1,var2)=var1+var2";
        var functions = new Dictionary<string, UserFunction>();
        var result = InputValidationService.FunctionValidation(input, functions);

        Assert.False(result);
    }

    [Fact]
    public void FunctionValidation_InvalidVariableNameInBody_ReturnsFalse()
    {
        var input = "funcName(var1,var2)=var1+3var2";
        var functions = new Dictionary<string, UserFunction>();
        var result = InputValidationService.FunctionValidation(input, functions);

        Assert.False(result);
    }

    [Fact]
    public void FunctionValidation_MissingEqualSign_ReturnsFalse()
    {
        var input = "funcName(var1,var2)var1+var2";
        var functions = new Dictionary<string, UserFunction>();
        var result = InputValidationService.FunctionValidation(input, functions);

        Assert.False(result);
    }

    [Fact]
    public void FunctionValidation_MissingComa_ReturnsFalse()
    {
        var input = "funcName(var1,var2=var1+var2";
        var functions = new Dictionary<string, UserFunction>();
        var result = InputValidationService.FunctionValidation(input, functions);

        Assert.False(result);
    }

    [Fact]
    public void FunctionValidation_UndefinedVariableInBody_ReturnsFalse()
    {
        var input = "funcName(var1)=var1+var2";
        var functions = new Dictionary<string, UserFunction>();
        var result = InputValidationService.FunctionValidation(input, functions);

        Assert.False(result);
    }

    [Fact]
    public void FunctionValidation_UnusedVariableInDefinition_ReturnsFalse()
    {
        var input = "funcName(var1,var2)=var1+5";
        var functions = new Dictionary<string, UserFunction>();
        var result = InputValidationService.FunctionValidation(input, functions);

        Assert.False(result);
    }

    [Fact]
    public void VariableValidation_PositiveNumber_ReturnsTrue()
    {
        var input = "x=5";
        var result = InputValidationService.VariableValidation(input);

        Assert.True(result);
    }

    [Fact]
    public void VariableValidation_NegativeNumber_ReturnsTrue()
    {
        var input = "x=-5";
        var result = InputValidationService.VariableValidation(input);

        Assert.True(result);
    }

    [Fact]
    public void VariableValidation_FractionalNumber_ReturnsTrue()
    {
        var input = "x=5.6";
        var result = InputValidationService.VariableValidation(input);

        Assert.True(result);
    }

    [Fact]
    public void VariableValidation_InvalidVariableName_ReturnsFalse()
    {
        var input = "1x=4";
        var result = InputValidationService.VariableValidation(input);

        Assert.False(result);
    }

    [Fact]
    public void VariableValidation_MissingEqualSign_ReturnsFalse()
    {
        var input = "x4";
        var result = InputValidationService.VariableValidation(input);

        Assert.False(result);
    }
}
