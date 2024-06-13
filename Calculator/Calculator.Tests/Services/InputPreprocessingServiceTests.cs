using NUnit.Framework;
using System;
using System.Collections.Generic;
using Calculator.Models;
using Calculator.Services;

[TestFixture]
public class InputPreprocessingServiceTests
{
    private InputPreprocessingService service;

    [SetUp]
    public void SetUp()
    {
        service = new InputPreprocessingService();
    }

    [Test]
    public void ProcessFunction_ValidFunction_ReturnsUserFunction()
    {
        string inputFunc = "f(x, y) = x + y";
        UserFunction result = service.ProcessFunction(inputFunc);
        Assert.IsNotNull(result);
        Assert.AreEqual("f", result.Name);
        Assert.AreEqual(2, result.Variables.Count);
        Assert.AreEqual("x", result.Variables[0]);
        Assert.AreEqual("y", result.Variables[1]);
        Assert.AreEqual("x + y", result.Expression);
    }

    [Test]
    public void ProcessFunction_InvalidFunction_ThrowsArgumentException()
    {
        string inputFunc = "invalid function";
        UserFunction result = service.ProcessFunction(inputFunc);
        Assert.IsNull(result);
    }

    [Test]
    public void ReplaceUserFunctions_ValidInput_ReplacesFunctionCalls()
    {
        var functions = new Dictionary<string, UserFunction> {
            { "f", new UserFunction { Name = "f", Variables = new List<string> { "x", "y" }, Expression = "x + y" } }
        };
        string input = "3 + f(1, 2)";
        string result = service.ReplaceUserFunctions(input, functions);
        Assert.AreEqual("3 + (1 + 2)", result);
    }

    [Test]
    public void ReplaceUserFunctions_MismatchedArgumentCount_ThrowsArgumentException()
    {
        var functions = new Dictionary<string, UserFunction>
        {
            { "f", new UserFunction { Name = "f", Variables = new List<string> { "x", "y" }, Expression = "x + y" } }
        };
        string input = "3 + f(1)";
        string result = service.ReplaceUserFunctions(input, functions);
        Assert.IsTrue(result.Contains("Function f expects 2 arguments, but got 1."));
    }

    [Test]
    public void ReplaceUserFunctions_NoFunctionCall_ReturnsOriginalInput()
    {
        var functions = new Dictionary<string, UserFunction>();
        string input = "3 + 5";
        string result = service.ReplaceUserFunctions(input, functions);
        Assert.AreEqual("3 + 5", result);
    }
}
