namespace CalculatorApp.MathLibrary.Entities.Operands;

public class PowerOperation : Term
{
    public uint Exponent { get; init; }

    public PowerOperation(uint exponent, IList<Term> innerTerms) : base(innerTerms)
    {
        Exponent = exponent;
    }

    public override decimal GetResult()
    {
        throw new NotImplementedException();
    }
}
