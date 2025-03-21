namespace CalculatorApp.MathLibrary.Entities.Operands;

public class FactorialOperation : Term
{
    public FactorialOperation(IList<Term> innerTerms) : base(innerTerms)
    {
    }

    public override decimal GetResult()
    {
        throw new NotImplementedException();
    }
}
