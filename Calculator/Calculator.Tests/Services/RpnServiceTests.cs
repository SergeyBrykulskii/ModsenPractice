using Calculator.Services;

namespace Calculator.Tests.Services;

public class RpnServiceTests
{

    [Fact]
    public void IsOperator_ShouldReturnTrue_ForValidOperators()
    {
        var symbol1 = '(';
        var symbol2 = '+';
        var symbol3 = '-';
        var symbol4 = '*';
        var symbol5 = '/';

        var result1 = RpnService.IsOperator(symbol1);
        var result2 = RpnService.IsOperator(symbol2);
        var result3 = RpnService.IsOperator(symbol3);
        var result4 = RpnService.IsOperator(symbol4);
        var result5 = RpnService.IsOperator(symbol5);

        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
        Assert.True(result4);
        Assert.True(result5);
    }

    [Fact]
    public void IsOperator_ShouldReturnFalse_ForInvalidOperators()
    {
        var symbol1 = 'a';
        var symbol2 = '1';
        var symbol3 = ' ';
        var symbol4 = '$';

        var result1 = RpnService.IsOperator(symbol1);
        var result2 = RpnService.IsOperator(symbol2);
        var result3 = RpnService.IsOperator(symbol3);
        var result4 = RpnService.IsOperator(symbol4);

        Assert.False(result1);
        Assert.False(result2);
        Assert.False(result3);
        Assert.False(result4);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn()
    {
        var rpn = new RpnService();
        var input = "5+(3-2)*4";
        var expectedOutput = "5 3 2 - 4 * + ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn_WithFractionalNumbers()
    {
        var rpn = new RpnService();
        var input = "(2.5+1.5)*3.2/2";
        var expectedOutput = "2.5 1.5 + 3.2 * 2 / ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn_WithNegativeNumbers()
    {
        var rpn = new RpnService();
        var input = "-5+(-3)*2";
        var expectedOutput = "0 5 - 0 3 - 2 * + ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn_WithFractionalAndNegativeNumbers()
    {
        var rpn = new RpnService();
        var input = "(-2.5+1.3)/8";
        var expectedOutput = "0 2.5 - 1.3 + 8 / ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn_ComplexExpression1()
    {
        var rpn = new RpnService();
        var input = "((4*3)/(2+5))-(6/(2-1))";
        var expectedOutput = "4 3 * 2 5 + / 6 2 1 - / - ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn_ComplexExpression2()
    {
        var rpn = new RpnService();
        var input = "((-4-2)*((8+3)/(2-1.67)))+(6/(2+(4/2)))";
        var expectedOutput = "0 4 - 2 - 8 3 + 2 1.67 - / * 6 2 4 2 / + / + ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn_ComplexExpression3()
    {
        var rpn = new RpnService();
        var input = "-2.3535*((3.32+1.67)/-0.8)+(6.48/(-2+(20/-2)))";
        var expectedOutput = "0 2.3535 3.32 1.67 + 0 0.8 - / * - 6.48 0 2 - 20 0 2 - / + / + ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn_ComplexExpression4()
    {
        var rpn = new RpnService();
        var input = "(((15+4)*(12-8))/(20+5))-((18-10)*(7+3))";
        var expectedOutput = "15 4 + 12 8 - * 20 5 + / 18 10 - 7 3 + * - ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn_ComplexExpression5()
    {
        var rpn = new RpnService();
        var input = "(-25-(13.66*2))+((18+14)/(16-10))";
        var expectedOutput = "0 25 - 13.66 2 * - 18 14 + 16 10 - / + ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn_ComplexExpression6()
    {
        var rpn = new RpnService();
        var input = "(((11*3)+7)/(15-10))-((16+17)*(20-12))";
        var expectedOutput = "11 3 * 7 + 15 10 - / 16 17 + 20 12 - * - ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }
}
