namespace CalculatorApp.MathLibrary.Entities.Operands;

public class AbsOperation : Term
{
    public AbsOperation(IList<Term> terms) : base(terms)
    {
        //if (terms.Count > 1) throw new NotSupportedException();
    }

    public override decimal GetResult()
    {
        if (InnerTerms.Count == 0)
            return 0;

        if (InnerTerms.Count > 1)
            throw new NotSupportedException();

        decimal result = InnerTerms.First().GetResult();
        if (result < 0)
            result *= -1;
        return result;
    }
}
