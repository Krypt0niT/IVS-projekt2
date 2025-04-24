namespace CalculatorApp.MathLibrary.Entities.Operands;

/// <summary>
/// Represents a modulo operation (a % b).
/// </summary>
public class ModuloOperation : Term
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModuloOperation"/> class.
    /// </summary>
    /// <param name="terms">The terms for the modulo operation (should contain exactly two).</param>
    public ModuloOperation(IList<Term> terms) : base(terms) {}

    /// <summary>
    /// Calculates the result of the modulo operation.
    /// </summary>
    /// <returns>The remainder of the division of the first term by the second.</returns>
    /// <exception cref="NotSupportedException">Thrown if the number of terms is not exactly 2 (except for 0 or 1).</exception>
    /// <exception cref="DivideByZeroException">Thrown if the second term is zero.</exception>
    public override decimal GetResult()
    {
        if (InnerTerms!.Count == 0)
            return 0;

        if (InnerTerms.Count == 1)
            return InnerTerms.First().GetResult();

        if (InnerTerms.Count != 2)
            throw new NotSupportedException();

        if (InnerTerms.Last().GetResult() == 0)
            throw new DivideByZeroException();

        decimal result = 0;
        result = InnerTerms.First().GetResult() % InnerTerms.Last().GetResult();
        return result;

    }
}
