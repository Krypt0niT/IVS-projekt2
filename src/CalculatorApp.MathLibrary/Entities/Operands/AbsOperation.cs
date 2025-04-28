using CalculatorApp.MathLibrary.LateXStyles;

namespace CalculatorApp.MathLibrary.Entities.Operands;

/// <summary>
/// Represents an absolute value operation (|x|).
/// </summary>
public class AbsOperation : Term
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AbsOperation"/> class.
    /// </summary>
    /// <param name="terms">A single term to get the absolute value of.</param>
    public AbsOperation(IList<Term> terms) : base(terms){}

    public override string GetLateX()
    {
        if (InnerTerms.Count == 0 || InnerTerms.Count > 1) throw new NotSupportedException();

        var childLatex = InnerTerms.First().GetLateX();

        if (IsSelected) return $"\\colorbox{{{Colors.Highlight}}}{{|}}" + childLatex + $"\\colorbox{{{Colors.Highlight}}}{{|}}";
        else return $"|{childLatex}|";
    }

    /// <summary>
    /// Calculates the absolute value of the single input term.
    /// </summary>
    /// <returns>The absolute value.</returns>
    /// <exception cref="NotSupportedException">Thrown if more than one term is provided.</exception>
    public override decimal GetResult()
    {
        if (InnerTerms.Count == 0)
            return 0;

        if (InnerTerms.Count > 1)
            throw new NotSupportedException();

        decimal result = InnerTerms.First().GetResult();
        if (result < 0)
            result *= -1;
        return result;
    }
}
