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

    /// <summary>
    /// Calculates the result of dividing the terms in sequence.
    /// </summary>
    /// <returns>The result of the division.</returns>
    /// <exception cref="DivideByZeroException">Thrown if there would be division by zero.</exception>
    public override decimal GetResult()
    {
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
