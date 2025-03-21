namespace CalculatorApp.MathLibrary.Entities.Operands;

public class AbsOperation : Term
{
    public AbsOperation(IList<Term> terms) : base(terms)
    {
    }

    public override decimal GetResult()
    {
        throw new NotImplementedException();
    }
}
