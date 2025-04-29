using CalculatorApp.MathLibrary.LateXStyles;

namespace CalculatorApp.MathLibrary.Entities.AbsoluteMembers;

public class BracketsMember : Term
{
    public BracketsMember(IList<Term>? terms = null) : base(terms)
    {
    }

    public override decimal GetResult()
    {
        if (InnerTerms == null || InnerTerms.Count != 1) throw new NotSupportedException();

        var result = InnerTerms.First().GetResult();
        return result;
    }

    public override string GetLateX()
    {
        if (InnerTerms == null || InnerTerms.Count != 1) throw new NotSupportedException();

        var innerLateX = InnerTerms.First().GetLateX();
        if (IsSelected) return $"\\colorbox{{{Colors.Highlight}}}{{(}}{innerLateX}\\colorbox{{{Colors.Highlight}}}{{)}}";
        return $"({innerLateX})";
    }
}
