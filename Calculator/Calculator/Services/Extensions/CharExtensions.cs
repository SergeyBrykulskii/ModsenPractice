namespace Calculator.Services.Extensions;

public static class CharExtensions
{
    public static bool IsOperator(this char c)
    {
        return "()+-*/".Contains(c);
    }
}
