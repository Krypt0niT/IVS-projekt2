namespace CalculatorApp.MathLibrary.Entities.Operands;

public class DivOperation : Term
{
    public DivOperation(IList<Term> terms) : base(terms)
    {
    }

    public override decimal GetResult()
    {
        decimal result = 0;
        if (InnerTerms.Count == 0)
            return 0;

        if (InnerTerms.Count == 1)
        {
            result = InnerTerms.First().GetResult();
            return result;
        }

        result = InnerTerms.First().GetResult();
        for (int i = 1; i < InnerTerms.Count; i++)
        {
            result /= InnerTerms[i].GetResult();
        }

        return result;
    }
}
