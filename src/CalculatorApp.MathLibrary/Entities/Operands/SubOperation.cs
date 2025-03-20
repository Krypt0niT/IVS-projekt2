namespace CalculatorApp.MathLibrary.Entities.Operands;

public class SubOperation : Term
{
    public SubOperation(IList<Term> innerTerms) : base(innerTerms)
    {
    }

    public override decimal GetResult()
    {
        throw new NotImplementedException();
    }
}
