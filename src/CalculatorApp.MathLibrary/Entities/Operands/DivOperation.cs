namespace CalculatorApp.MathLibrary.Entities.Operands;

public class DivOperation : Term
{
    public DivOperation(IList<Term> terms) : base(terms)
    {
    }

    public override decimal GetResult()
    {
        throw new NotImplementedException();
    }
}
