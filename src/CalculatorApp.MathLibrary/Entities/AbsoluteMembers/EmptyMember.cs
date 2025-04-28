using CalculatorApp.MathLibrary.LateXStyles;

namespace CalculatorApp.MathLibrary.Entities.AbsoluteMembers;

public class EmptyMember : Term
{
    public EmptyMember(IList<Term>? terms = null) : base(terms)
    {
    }

    public override decimal GetResult()
    {
        throw new Exception("Chýbajúca hodnota.");
    }

    public override string GetLateX()
    {
        if (IsSelected) return $"\\color{{{Colors.Highlight}}}{{_}}";
        return "_";
    }
}
