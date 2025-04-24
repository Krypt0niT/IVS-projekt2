using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;

namespace CalculatorApp.MathLibrary.Entities.Operands;

/// <summary>
/// Represents an n-th root operation (e.g. square root, cube root) on a single term.
/// </summary>
public class NthRootOperation : Term
{
    /// <summary>
    /// The degree of the root (e.g. 2 for square root).
    /// </summary>
    public int Degree { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NthRootOperation"/> class.
    /// </summary>
    /// <param name="degree">The root degree.</param>
    /// <param name="innerTerms">The term to apply the root to (should contain exactly one).</param>
    public NthRootOperation(int degree, IList<Term> innerTerms) : base(innerTerms)
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

        decimal input = InnerTerms.First().GetResult();

        if (input < 0 && Degree % 2 == 0)
            throw new NotSupportedException();

        if (input == 0)
            return 0;

        return NthRoot(input, Degree, 0.000001m); // precision = 1e-6
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
        if (n == 0)
            throw new NotSupportedException();

        decimal x = value / n;
        decimal prev = 0;

        while (new AbsOperation(
            new List<Term> {
                new SubOperation(new List<Term> {
                    new AbsoluteMember(x),
                    new AbsoluteMember(prev)
                })
            }
        ).GetResult() > precision)
        {
            prev = x;

            var pow = new PowerOperation(n - 1, new List<Term> {
            new AbsoluteMember(x)
        }).GetResult();

            x = ((n - 1) * x + value / pow) / n;
        }

        return x;
    }

}
