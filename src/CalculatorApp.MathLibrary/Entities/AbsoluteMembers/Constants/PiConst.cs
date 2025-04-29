using CalculatorApp.MathLibrary.LateXStyles;

namespace CalculatorApp.MathLibrary.Entities.AbsoluteMembers;

/// <summary>
/// Represents the mathematical constant π (pi).
/// </summary>
public class PiConst : Term
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PiConst"/> class.
    /// </summary>
    /// <param name="terms">Optional list of inner terms (not used).</param>
    public PiConst(IList<Term>? terms = null) : base(terms)
    {
    }

    public override string GetLateX()
    {
        if (IsSelected) return $"\\colorbox{{{Colors.Highlight}}}{{\\pi}}";
        return $"\\pi";
    }

    // <summary>
    /// Returns the decimal approximation of the constant π (pi).
    /// </summary>
    /// <returns>Decimal value of π (pi), approximately 3.14159265358979.</returns>
    public override decimal GetResult()
    {
        return 3.14159265358979m;
    }
}
