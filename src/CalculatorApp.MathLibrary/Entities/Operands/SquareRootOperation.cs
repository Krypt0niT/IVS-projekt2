using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;

namespace CalculatorApp.MathLibrary.Entities.Operands;

public class SquareRootOperation : Term
{
    public uint Degree { get; init; }

    public SquareRootOperation(uint degree, IList<Term> innerTerms) : base(innerTerms)
    {
        Degree = degree;
    }

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

    private decimal NthRoot(decimal value, uint n, decimal precision)
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
