namespace CalculatorApp.MathLibrary.Entities.Operands;

public class ModuloOperation : Term
{
    public ModuloOperation(IList<Term> terms) : base(terms)
    {
    }

    public override decimal GetResult()
    {
        throw new NotImplementedException();
    }
}
