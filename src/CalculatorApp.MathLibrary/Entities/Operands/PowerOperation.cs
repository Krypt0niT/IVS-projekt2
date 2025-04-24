namespace CalculatorApp.MathLibrary.Entities.Operands;

/// <summary>
/// Represents an exponentiation operation (x^n).
/// </summary>
public class PowerOperation : Term
{
    /// <summary>
    /// The exponent value.
    /// </summary>
    public uint Exponent { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PowerOperation"/> class.
    /// </summary>
    /// <param name="exponent">The exponent to raise the base to.</param>
    /// <param name="innerTerms">The base value (should contain exactly one).</param>
    public PowerOperation(uint exponent, IList<Term> innerTerms) : base(innerTerms)
    {
        Exponent = exponent;
    }

    /// <summary>
    /// Calculates the result of raising the base to the exponent.
    /// </summary>
    /// <returns>The result of the power operation.</returns>
    public override decimal GetResult()
    {
        if (InnerTerms!.Count == 0)
            return 0;

        if (InnerTerms.Count != 1)
            throw new NotSupportedException();

        decimal baseValue = InnerTerms.First().GetResult();

        return DecimalPow(baseValue, Exponent);
    }

    /// <summary>
    /// Performs exponentiation using a simple loop.
    /// </summary>
    /// <param name="baseValue">Base value.</param>
    /// <param name="exponent">Exponent.</param>
    /// <returns>Result of base^exponent.</returns>
    private decimal DecimalPow(decimal baseValue, uint exponent)
    {
        decimal result = 1;

        for (uint i = 0; i < exponent; i++)
        {
            result *= baseValue;
        }

        return result;
    }
}
