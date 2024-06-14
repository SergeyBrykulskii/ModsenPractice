using Calculator.Services;

namespace Calculator.Tests.Services;

public class RpnServiceTests
{
    [Fact]
    public void CalculateRpn_WithPositiveIntengerNumbersAndMixedOperations_ReturnsCorrectResult1()
    {
        var service = new RpnService();
        var input = "2 5 3 * +";

        var result = service.СalculateRpn(input);

        Assert.Equal(17,result);
    }
   
     [Fact]
    public void CalculateRpn_WithPositiveIntengerNumbersAndMixedOperations_ReturnsCorrectResult2()
    {
        var service = new RpnService();
        var input = " 5 3 2 - 4 * + ";

        var result = service.СalculateRpn(input);

        Assert.Equal(9, result);
    }


    [Fact]
    public void CalculateRpn_WithPositiveDoubleNumbersAndMixedOperations_ReturnsCorrectResult()
    {
        var service = new RpnService();
        var input = " 2.5 1.5 + 3.2 * 2 / ";

        var result = service.СalculateRpn(input);

        Assert.Equal(6.4, result);
    }

    [Fact]
    public void CalculateRpn_WithNegativeDoubleNumbersAndMixedOperations_ReturnsCorrectResult()
    {
        var service = new RpnService();
        var input = "0 2.5 - 1.3 + 8 /";

        var result = service.СalculateRpn(input);

        Assert.Equal(-0.15, result);
    }

    [Fact]
    public void CalculateRpn_WithPositiveIntengerNumbersAndMixedOperations_ReturnsCorrectResult()
    {
        var service = new RpnService();
        var input = "4 3 * 2 5 + / 6 2 1 - / -";

        var result = service.СalculateRpn(input);

        Assert.Equal(-4.285714, result,0.000001);
       
    }

    [Fact]
    public void CalculateRpn_WithDoubleNumbersAndMixedOperationsMoreComplex_ReturnsCorrectResult()
    {
        var service = new RpnService();
        var input = " 0 4 - 2 - 8 3 + 2 1.67 - / *6 2 4 2 / + / +";
        
        var result = service.СalculateRpn(input);

        Assert.Equal(-198.5, result,0.000001);
    }

    [Fact]
    public void CalculateRpn_WithDoubleNumbersAndMixedOperationsMoreComplex_ReturnsCorrectResult2()
    {
        var service = new RpnService();
        var input = "0 2.3535 3.32 1.67 + 0 0.8 - / * - 6.48 0 2 - 20 0 2 - / + / +";

        var result = service.СalculateRpn(input);

        Assert.Equal(14.1399562, result, 0.000001);
    }

    [Fact]
    public void CalculateRpn_WithIntengerNumbersAndMixedOperationsMoreComplex_ReturnsCorrectResult()
    {
        var service = new RpnService();
        var input = "11 3 * 7 + 15 10 - / 16 17 + 20 12 - * - ";

        var result = service.СalculateRpn(input);

        Assert.Equal(-256, result, 0.000001);
    }
}
