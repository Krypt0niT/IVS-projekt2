namespace CalculatorApp.MathLibrary.Entities.Operands;

/// <summary>
/// Represents a mathematical summation operation (a + b + c + ...).
/// </summary>
/// <remarks>
/// The SumOperation class evaluates the sum of all inner terms provided.
/// </remarks>
public class SumOperation : Term
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SumOperation"/> class with a list of terms to sum.
    /// </summary>
    /// <param name="terms">A list of terms that will be summed together.</param>
    public SumOperation(IList<Term> terms) : base(terms)
    {
    }

    public override string GetLateX()
    {
        var result = "";
        if (InnerTerms == null) throw new Exception();
        if (InnerTerms.Count == 1) return InnerTerms.First().GetLateX();

        foreach (var a in InnerTerms)
        {
            result += a.GetLateX() + "+";
        }
        result = result.Substring(0, result.Length - 1); // removes +
        
        return result;
    }

    /// <summary>
    /// Calculates the result of the sum operation.
    /// </summary>
    /// <returns>The total sum of all inner term results.</returns>
    public override decimal GetResult()
    {
        decimal result = 0;

        foreach (var term in InnerTerms!)
        {
            result += term.GetResult();
        }

        return result;
    }
}
