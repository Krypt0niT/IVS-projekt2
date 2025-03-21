namespace CalculatorApp.MathLibrary.Entities.AbsoluteMembers;

public class PiConst : Term
{
    public PiConst(IList<Term>? terms = null) : base(terms)
    {
    }

    public override decimal GetResult()
    {
        throw new NotImplementedException();
    }
}
