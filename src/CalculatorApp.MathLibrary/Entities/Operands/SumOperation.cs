namespace CalculatorApp.MathLibrary.Entities.Operands;

public class SumOperation : Term
{
    public SumOperation(IList<Term> terms) : base(terms)
    {
    }

    public override decimal GetResult()
    {
        decimal result = 0;

        foreach (var term in InnerTerms)
        {
            result += term.GetResult();
        }

        return result;
    }
}
