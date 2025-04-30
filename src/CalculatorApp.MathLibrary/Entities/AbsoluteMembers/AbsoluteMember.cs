using CalculatorApp.MathLibrary.LateXStyles;

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

    public override string GetLateX()
    {
        if (IsSelected) return $"\\colorbox{{{Colors.Highlight}}}{{{AbsoluteValue.ToString()}}}";
        return AbsoluteValue.ToString();
    }
}
