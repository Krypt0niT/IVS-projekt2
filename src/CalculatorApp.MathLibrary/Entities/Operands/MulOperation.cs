using static System.Net.Mime.MediaTypeNames;

namespace CalculatorApp.MathLibrary.Entities.Operands;

/// <summary>
/// Represents a multiplication operation (a * b * c * ...).
/// </summary>
public class MulOperation : Term
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MulOperation"/> class.
    /// </summary>
    /// <param name="innerTerms">The terms to multiply.</param>
    public MulOperation(IList<Term> innerTerms) : base(innerTerms){}

    public override string GetLateX()
    {
        var result = "";
        if (InnerTerms == null) throw new Exception();
        if (InnerTerms.Count == 1) return InnerTerms.First().GetLateX();

        foreach(var a in InnerTerms)
        {
            result += a.GetLateX() + "\\cdot";
        }
        result = result.Substring(0, result.Length - 5); // removes \cdot

        return result;
    }

    /// <summary>
    /// Calculates the product of all terms.
    /// </summary>
    /// <returns>The result of multiplication.</returns>
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
            result *= InnerTerms[i].GetResult();
        }

        return result;
    }
}
