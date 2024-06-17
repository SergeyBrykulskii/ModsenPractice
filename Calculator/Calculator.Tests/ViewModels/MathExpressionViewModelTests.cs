using Calculator.ViewModels;

namespace Calculator.Tests.ViewModels;

public class MathExpressionViewModelTests
{
    private readonly MathExpressionViewModel _viewModel = new();

    [Fact]
    public void AddCharCommand_EmptyExpression_AddsCharToMathExpression()
    {
        // Arrange
        var expected = "123";

        // Act
        _viewModel.AddCharCommand.Execute("1");
        _viewModel.AddCharCommand.Execute("2");
        _viewModel.AddCharCommand.Execute("3");

        // Assert
        Assert.Equal(expected, _viewModel.MathExpression);
    }

    [Fact]
    public void DeleteCharCommand_NotEmptyExpression_DeletesLastCharFromMathExpression()
    {
        // Arrange
        var expected = "12";
        _viewModel.MathExpression = "123";

        // Act
        _viewModel.DeleteCharCommand.Execute(null);

        // Assert
        Assert.Equal(expected, _viewModel.MathExpression);
    }

    [Fact]
    public void ClearEntryCommand_FilledFields_ClearsMathExpressionAndCalculationResult()
    {
        // Arrange
        var expectedFieldState = "";
        _viewModel.MathExpression = "123";
        _viewModel.CalculationResult = "456";

        // Act
        _viewModel.ClearEntryCommand.Execute(null);

        // Assert
        Assert.Equal(expectedFieldState, _viewModel.MathExpression);
        Assert.Equal(expectedFieldState, _viewModel.CalculationResult);
    }

    [Fact]
    public void AddFunctionCommand_CorrectFunction_AddsFunctionToUserFunctionsList()
    {
        // Arrange
        var expected = "f(x,y) = x + y";

        // Act
        _viewModel.MathExpression = expected;
        _viewModel.AddFunctionCommand.Execute(null);

        // Assert
        Assert.Contains(expected, _viewModel.UserFunctionsList);
    }

    [Fact]
    public void AddVariableCommand_CorrectVariable_AddsVariableToUserVariablesList()
    {
        // Arrange
        var expected = "x = 2";

        // Act
        _viewModel.MathExpression = expected;
        _viewModel.AddVariableCommand.Execute(null);

        // Assert
        Assert.Contains(expected, _viewModel.UserVariablesList);
    }

    [Fact]
    public void CalculateExpressionCommand_CorrectExpression_CalculatesExpressionAndSetsCalculationResult()
    {
        // Arrange
        var mathExpression = "2 + 2";
        var expectedCalculationResult = "4";

        // Act
        _viewModel.MathExpression = mathExpression;
        _viewModel.CalculateExpressionCommand.Execute(null);

        // Assert
        Assert.Equal(expectedCalculationResult, _viewModel.CalculationResult);
    }

    [Fact]
    public void AddFunctionCommand_InvalidFunction_DoesNotAddFunctionToUserFunctionsList()
    {
        // Arrange
        var invalidFunction = "f(x,y) = x +";

        // Act
        _viewModel.MathExpression = invalidFunction;
        _viewModel.AddFunctionCommand.Execute(null);

        // Assert
        Assert.DoesNotContain(invalidFunction, _viewModel.UserFunctionsList);
        Assert.Equal("Invalid input", _viewModel.CalculationResult);
    }

    [Fact]
    public void AddVariableCommand_InvalidVariable_DoesNotAddVariableToUserVariablesList()
    {
        // Arrange
        var invalidVariable = "x =";

        // Act
        _viewModel.MathExpression = invalidVariable;
        _viewModel.AddVariableCommand.Execute(null);

        // Assert
        Assert.DoesNotContain(invalidVariable, _viewModel.UserVariablesList);
        Assert.Equal("Invalid input", _viewModel.CalculationResult);
    }

    [Fact]
    public void CalculateExpressionCommand_InvalidExpression_DoesNotCalculateExpressionAndSetsCalculationResult()
    {
        // Arrange
        var invalidExpression = "2 + + 2";
        var expectedCalculationResult = "Invalid input";

        // Act
        _viewModel.MathExpression = invalidExpression;
        _viewModel.CalculateExpressionCommand.Execute(null);

        // Assert
        Assert.Equal(expectedCalculationResult, _viewModel.CalculationResult);
    }

}
