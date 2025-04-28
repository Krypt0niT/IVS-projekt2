using CalculatorApp.MathLibrary.LateXStyles;

namespace CalculatorApp.MathLibrary.Entities.Operands;

/// <summary>
/// Represents a factorial operation (n!).
/// </summary>
public class FactorialOperation : Term
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FactorialOperation"/> class.
    /// </summary>
    /// <param name="innerTerms">Should contain one whole, non-negative number.</param>
    public FactorialOperation(IList<Term> innerTerms) : base(innerTerms){}

    public override string GetLateX()
    {
        if (InnerTerms.Count > 1)
            throw new NotSupportedException();

        if (InnerTerms.Count == 0) return $"!";

        var childLatex = InnerTerms.First().GetLateX();

        if (IsSelected) return $"{childLatex}\\color{{{Colors.Highlight}}}{{!}}";
        else return $"{childLatex}!";
    }

    /// <summary>
    /// Calculates the factorial of a single term.
    /// </summary>
    /// <returns>The factorial result.</returns>
    /// <exception cref="NotSupportedException">If the input is negative or not a whole number.</exception>
    public override decimal GetResult()
    {
        if (InnerTerms!.Count == 0)
            return 0;

        if (InnerTerms.Count != 1)
            throw new NotSupportedException();

        decimal input = InnerTerms.First().GetResult();

        if (input < 0 || input % 1 != 0)
            throw new NotSupportedException();

        int n = (int)input;

        decimal result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }
}
