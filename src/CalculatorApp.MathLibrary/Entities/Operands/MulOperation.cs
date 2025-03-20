namespace CalculatorApp.MathLibrary.Entities.Operands;

public class MulOperation : Term
{
    public MulOperation(IList<Term> innerTerms) : base(innerTerms)
    {
    }

    public override decimal GetResult()
    {
        throw new NotImplementedException();
    }
}
