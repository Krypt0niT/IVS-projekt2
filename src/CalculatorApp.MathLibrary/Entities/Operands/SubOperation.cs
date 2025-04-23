namespace CalculatorApp.MathLibrary.Entities.Operands;

public class SubOperation : Term
{
    public SubOperation(IList<Term> innerTerms) : base(innerTerms)
    {
    }

    public override decimal GetResult()
    {
        if (InnerTerms.Count == 0)
            return 0;

        decimal result = InnerTerms[0].GetResult();
        for (int i = 1; i < InnerTerms.Count; i++)
        {
            result -= InnerTerms[i].GetResult();
        }

        return result;
    }
}
