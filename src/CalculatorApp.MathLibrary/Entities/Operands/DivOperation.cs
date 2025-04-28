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
        decimal result = 0;
        if (InnerTerms == null || InnerTerms.Count != 2) throw new Exception("Chybajúce údaje v zlomku.");
        
        if (InnerTerms[1].GetResult() == 0)
            throw new DivideByZeroException("Výsledok pod zlomkom nemôže byť nula.");

            result = InnerTerms[0].GetResult() / InnerTerms[1].GetResult();

        return result;
    }
}
