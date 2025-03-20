namespace CalculatorApp.MathLibrary.Entities.Operands;

public class AbsoluteMember : Term
{
    private decimal AbsoluteValue { get; }

    public AbsoluteMember(decimal absoluteValue, IList<Term>? terms = null) : base(terms)
    {
        AbsoluteValue = absoluteValue;
    }

    public override decimal GetResult()
    {
        return AbsoluteValue;
    }
}
