namespace CalculatorApp.MathLibrary.Entities.AbsoluteMembers;

public class AbsoluteMember : Term
{
    public decimal AbsoluteValue { get; init; }

    public AbsoluteMember(decimal absoluteValue, IList<Term>? terms = null) : base(terms)
    {
        AbsoluteValue = absoluteValue;
    }

    public override decimal GetResult()
    {
        return AbsoluteValue;
    }
}
