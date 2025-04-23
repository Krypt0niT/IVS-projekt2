namespace CalculatorApp.MathLibrary.Entities.Operands;

public class AbsOperation : Term
{
    public AbsOperation(IList<Term> terms) : base(terms)
    {
        if (terms.Count > 1) throw new NotSupportedException();
    }

    public override decimal GetResult()
    {
        throw new NotImplementedException();
    }
}
