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
        throw new NotImplementedException();
    }
}
