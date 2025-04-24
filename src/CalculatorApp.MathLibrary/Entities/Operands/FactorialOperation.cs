namespace CalculatorApp.MathLibrary.Entities.Operands;

public class FactorialOperation : Term
{
    public FactorialOperation(IList<Term> innerTerms) : base(innerTerms)
    {
    }

    public override decimal GetResult()
    {
        if (InnerTerms!.Count == 0)
            return 0;

        if (InnerTerms.Count != 1)
            throw new NotSupportedException();

        decimal input = InnerTerms.First().GetResult();

        if (input < 0 || input % 1 != 0)
            throw new NotSupportedException();

        int n = (int)input;

        decimal result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }
}
