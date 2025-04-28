using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.LateXStyles;

namespace CalculatorApp.MathLibrary.Entities.Operands;

/// <summary>
/// Represents a division operation (a / b / c ...).
/// </summary>
public class DivOperation : Term
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DivOperation"/> class.
    /// </summary>
    /// <param name="terms">The terms to be divided.</param>
    public DivOperation(IList<Term> terms) : base(terms){}

    public override string GetLateX()
    {
        if (InnerTerms == null || InnerTerms.Count != 2) throw new NotSupportedException();

        if (IsSelected) return $"\\colorbox{{{Colors.Highlight}}}{{\\frac{{{InnerTerms[0].GetLateX()}}}{{{InnerTerms[1].GetLateX()}}}}}";

        return $"\\frac{{{InnerTerms[0].GetLateX()}}}{{{InnerTerms[1].GetLateX()}}}";
        throw new NotImplementedException();
    }

    /// <summary>
    /// Calculates the result of dividing the terms in sequence.
    /// </summary>
    /// <returns>The result of the division.</returns>
    /// <exception cref="DivideByZeroException">Thrown if there would be division by zero.</exception>
    public override decimal GetResult()
    {
        // TODO: bude vzdy presne 2
        // TODO: cele prerobit na zlomok nie na delenie
        decimal result = 0;
        if (InnerTerms.Count == 0)
            return 0;

        if (InnerTerms.Count == 1)
        {
            result = InnerTerms.First().GetResult();
            return result;
        }

        result = InnerTerms.First().GetResult();
        for (int i = 1; i < InnerTerms.Count; i++)
        {
            if (InnerTerms[i].GetResult() == 0)
                throw new DivideByZeroException();

            result /= InnerTerms[i].GetResult();
        }

        return result;
    }
}
