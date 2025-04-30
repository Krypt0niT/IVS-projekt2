using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.LateXStyles;

namespace CalculatorApp.MathLibrary.Entities.Operands;

/// <summary>
/// Represents an n-th root operation (e.g. square root, cube root) on a single term.
/// </summary>
public class NthRootOperation : Term
{
    /// <summary>
    /// The degree of the root (e.g. 2 for square root).
    /// </summary>
    public Term Degree { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NthRootOperation"/> class.
    /// </summary>
    /// <param name="degree">The root degree.</param>
    /// <param name="innerTerms">The term to apply the root to (should contain exactly one).</param>
    public NthRootOperation(Term degree, IList<Term> innerTerms) : base(innerTerms)
    {
        Degree = degree;
    }

    /// <summary>
    /// Calculates the result of the root operation.
    /// </summary>
    /// <returns>The computed root as a decimal.</returns>
    /// <exception cref="NotSupportedException">Thrown if input is invalid or unsupported.</exception>
    public override decimal GetResult()
    {
        if(InnerTerms!.Count == 0)
            return 0;
        
        if (InnerTerms!.Count != 1)
            throw new NotSupportedException();

        var degree = Degree.GetResult();

        if (degree < 0)
            throw new NotSupportedException();

        decimal input = InnerTerms.First().GetResult();

        if (input < 0 && degree % 2 == 0)
            throw new NotSupportedException("Odmocnina môže byť iba celočíselná.");

        if (input == 0)
            return 0;

        return NthRoot(input, (int)degree, 0.0000000000001m); // precision = 1e-6
    }

    /// <summary>
    /// Computes the n-th root of a given decimal value using Newton's method.
    /// </summary>
    /// <param name="value">The value to take the root of.</param>
    /// <param name="n">The degree of the root.</param>
    /// <param name="precision">Precision of approximation.</param>
    /// <returns>The computed root.</returns>
    private decimal NthRoot(decimal value, int n, decimal precision)
    {
        if (n == 0) throw new NotSupportedException();

        decimal x = value / n;
        decimal prev;
        decimal abs;
        do
        {
            prev = x;
            decimal pow = 1;

            for (int i = 0; i < n - 1; i++)
                pow *= x;

            x = ((n - 1) * x + value / pow) / n;

            if ((x - prev) < 0)
                abs = (x - prev) * (-1);
            else
                abs = x - prev;
        }
        while (abs > precision);

        return x;
    }

    public override string GetLateX()
    {
        if (InnerTerms == null || InnerTerms.Count != 1) throw new NotSupportedException();

        var degreeLateX = Degree.GetLateX();

        if (IsSelected)
        {
            return $"\\sqrt[\\colorbox{{{Colors.Highlight}}}{{{degreeLateX}}}]{{{InnerTerms.First().GetLateX()}}}";
        }
        else
        {
            if (Degree.GetType() == typeof(AbsoluteMember))
            {
                var absoluteMember = (AbsoluteMember)Degree;

                if (absoluteMember.AbsoluteValue == 2) return $"\\sqrt{{{InnerTerms.First().GetLateX()}}}";
            }
            
            return $"\\sqrt[{degreeLateX}]{{{InnerTerms.First().GetLateX()}}}";
        }
    }
}
