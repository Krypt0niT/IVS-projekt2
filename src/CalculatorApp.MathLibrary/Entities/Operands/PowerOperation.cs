using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.LateXStyles;

namespace CalculatorApp.MathLibrary.Entities.Operands;

/// <summary>
/// Represents an exponentiation operation (x^n), including support for negative exponents.
/// </summary>
public class PowerOperation : Term
{
    /// <summary>
    /// The exponent value.
    /// </summary>
    public Term Exponent { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PowerOperation"/> class.
    /// </summary>
    /// <param name="exponent">The exponent to raise the base to (can be negative).</param>
    /// <param name="innerTerms">The base value (should contain exactly one).</param>
    public PowerOperation(Term exponent, IList<Term> innerTerms) : base(innerTerms)
    {
        if (exponent.GetType() != typeof(AbsoluteMember) || exponent.GetType() != typeof(EmptyMember))
            throw new NotSupportedException("Exponent moze byt iba celočiselny alebo prazdny");
        Exponent = exponent;
    }

    /// <summary>
    /// Calculates the result of raising the base to the exponent.
    /// </summary>
    /// <returns>The result of the power operation.</returns>
    /// <exception cref="NotSupportedException">Thrown when multiple terms are passed in.</exception>
    /// <exception cref="DivideByZeroException">Thrown when attempting to raise 0 to a negative exponent.</exception>
    public override decimal GetResult()
    {
        if (InnerTerms!.Count == 0)
            return 0;

        if (InnerTerms.Count != 1)
            throw new NotSupportedException();

        decimal baseValue = InnerTerms.First().GetResult();

        if (Exponent.GetType() == typeof(AbsoluteMember))
        {
            var absoluteMember = (AbsoluteMember)Exponent;

            if (absoluteMember.AbsoluteValue % 1 != 0) 
                throw new NotSupportedException("V exponente sa možu nachádzať iba celé čisla");

            if (baseValue == 0 && absoluteMember.AbsoluteValue < 0)
                throw new DivideByZeroException();

            return DecimalPow(baseValue, (int)absoluteMember.AbsoluteValue);
        }
        throw new Exception("Chybajuci exponent");
    }

    /// <summary>
    /// Performs exponentiation for both positive and negative exponents using a simple loop.
    /// </summary>
    /// <param name="baseValue">Base value.</param>
    /// <param name="exponent">Exponent (can be negative).</param>
    /// <returns>Result of base^exponent.</returns>
    private decimal DecimalPow(decimal baseValue, int exponent)
    {
        decimal result = 1;
        decimal absExp;
        if (exponent >= 0)
            absExp = exponent;
        else
            absExp = -exponent;

        for (int i = 0; i < absExp; i++)
        {
            result *= baseValue;
        }

        if (exponent >= 0)
            return result;
        else
            return (1 / result);
    }

    public override string GetLateX()
    {
        if (InnerTerms == null || InnerTerms.Count != 1) throw new NotSupportedException();

        if (IsSelected)
        {
            return $"{InnerTerms.First().GetLateX()}^{{\\colorbox{{{Colors.Highlight}}}{{{Exponent.GetLateX()}}}}}";
        }
        else
        {
            return $"{InnerTerms.First().GetLateX()}^{{{Exponent.GetLateX()}}}";
        } 
    }
}
