using Calculator.Services;

namespace Calculator.Tests.Services;

public class RpnServiceTests
{
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
        var input = "(2,5+1,5)*3,2/2";
        var expectedOutput = "2,5 1,5 + 3,2 * 2 / ";

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
        var input = "(-2,5+1,3)/8";
        var expectedOutput = "0 2,5 - 1,3 + 8 / ";

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
        var input = "((-4-2)*((8+3)/(2-1,67)))+(6/(2+(4/2)))";
        var expectedOutput = "0 4 - 2 - 8 3 + 2 1,67 - / * 6 2 4 2 / + / + ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void InfixNotationToRpn_ShouldConvertInfixToRpn_ComplexExpression3()
    {
        var rpn = new RpnService();
        var input = "-2,3535*((3,32+1,67)/-0,8)+(6,48/(-2+(20/-2)))";
        var expectedOutput = "0 2,3535 3,32 1,67 + 0 0,8 - / * - 6,48 0 2 - 20 0 2 - / + / + ";

        var result = rpn.InfixNotationToRpn(input);

        Assert.Equal(expectedOutput, result);
    }
}
