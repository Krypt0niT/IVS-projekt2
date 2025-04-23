namespace CalculatorApp.MathLibrary.Entities.Operands;

public class SubOperation : Term
{
    public SubOperation(IList<Term> innerTerms) : base(innerTerms)
    {
    }

    public override decimal GetResult()
    {
        decimal result = 0;

        foreach (var term in InnerTerms!)
        {
            result -= term.GetResult();
        }

        return result;
    }
}
