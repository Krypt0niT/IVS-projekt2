namespace CalculatorApp.MathLibrary.Entities.Operands;

public class MulOperation : Term
{
    public MulOperation(IList<Term> innerTerms) : base(innerTerms)
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
            result *= InnerTerms[i].GetResult();
        }

        return result;
    }
}
