namespace CalculatorApp.MathLibrary.Entities.Operands;

public class ModuloOperation : Term
{
    public ModuloOperation(IList<Term> terms) : base(terms)
    {
    }

    public override decimal GetResult()
    {
        if (InnerTerms!.Count == 0)
            return 0;

        if (InnerTerms.Count == 1)
            return InnerTerms.First().GetResult();

        if (InnerTerms.Count != 2)
            throw new NotSupportedException();

        if (InnerTerms.Last().GetResult() == 0)
            throw new DivideByZeroException();

        decimal result = 0;
        result = InnerTerms.First().GetResult() % InnerTerms.Last().GetResult();
        return result;

    }
}
