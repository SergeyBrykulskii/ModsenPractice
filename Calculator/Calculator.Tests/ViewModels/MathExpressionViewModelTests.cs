using Calculator.ViewModels;

namespace Calculator.Tests.ViewModels;

public class MathExpressionViewModelTests
{
    private readonly MathExpressionViewModel _viewModel = new();

    [Fact]
    public void AddCharCommand_EmptyExpression_AddsCharToMathExpression()
    {
        var expected = "123";

        _viewModel.AddCharCommand.Execute("1");
        _viewModel.AddCharCommand.Execute("2");
        _viewModel.AddCharCommand.Execute("3");

        Assert.Equal(expected, _viewModel.MathExpression);
    }

    [Fact]
    public void DeleteCharCommand_NotEmptyExpression_DeletesLastCharFromMathExpression()
    {
        var expected = "12";
        _viewModel.MathExpression = "123";

        _viewModel.DeleteCharCommand.Execute(null);

        Assert.Equal(expected, _viewModel.MathExpression);
    }

    [Fact]
    public void ClearEntryCommand_FilledFields_ClearsMathExpressionAndCalculationResult()
    {
        var expectedFieldState = "";
        _viewModel.MathExpression = "123";
        _viewModel.CalculationResult = "456";

        _viewModel.ClearEntryCommand.Execute(null);

        Assert.Equal(expectedFieldState, _viewModel.MathExpression);
        Assert.Equal(expectedFieldState, _viewModel.CalculationResult);
    }

    [Fact]
    public void AddFunctionCommand_CorrectFunction_AddsFunctionToUserFunctionsList()
    {
        var expected = "f(x,y) = x + y";

        _viewModel.MathExpression = expected;
        _viewModel.AddFunctionCommand.Execute(null);

        Assert.Contains(expected, _viewModel.UserFunctionsList);
    }

    [Fact]
    public void AddVariableCommand_CorrectVariable_AddsVariableToUserVariablesList()
    {
        var expected = "x = 2";

        _viewModel.MathExpression = expected;
        _viewModel.AddVariableCommand.Execute(null);

        Assert.Contains(expected, _viewModel.UserVariablesList);
    }

    [Fact]
    public void CalculateExpressionCommand_CorrectExpression_CalculatesExpressionAndSetsCalculationResult()
    {
        var mathExpression = "2 + 2";
        var expectedCalculationResult = "4";

        _viewModel.MathExpression = mathExpression;
        _viewModel.CalculateExpressionCommand.Execute(null);

        Assert.Equal(expectedCalculationResult, _viewModel.CalculationResult);
    }

    [Fact]
    public void AddFunctionCommand_InvalidFunction_DoesNotAddFunctionToUserFunctionsList()
    {
        var invalidFunction = "f(x,y) = x +";

        _viewModel.MathExpression = invalidFunction;
        _viewModel.AddFunctionCommand.Execute(null);

        Assert.DoesNotContain(invalidFunction, _viewModel.UserFunctionsList);
        Assert.Equal("Invalid input", _viewModel.CalculationResult);
    }

    [Fact]
    public void AddVariableCommand_InvalidVariable_DoesNotAddVariableToUserVariablesList()
    {
        var invalidVariable = "x =";

        _viewModel.MathExpression = invalidVariable;
        _viewModel.AddVariableCommand.Execute(null);

        Assert.DoesNotContain(invalidVariable, _viewModel.UserVariablesList);
        Assert.Equal("Invalid input", _viewModel.CalculationResult);
    }

    [Fact]
    public void CalculateExpressionCommand_InvalidExpression_DoesNotCalculateExpressionAndSetsCalculationResult()
    {
        var invalidExpression = "2 + + 2";
        var expectedCalculationResult = "Invalid input";

        _viewModel.MathExpression = invalidExpression;
        _viewModel.CalculateExpressionCommand.Execute(null);

        Assert.Equal(expectedCalculationResult, _viewModel.CalculationResult);
    }
}
