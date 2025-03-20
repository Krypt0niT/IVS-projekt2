namespace CalculatorApp.MathLibrary.Entities;

public abstract class Term
{
    public Term(IList<Term> innerTerms)
    {
        InnerTerms = innerTerms;
    }

    public Term(decimal absoluteValue)
    {
        AbsoluteValue = absoluteValue;
    }

    protected decimal? AbsoluteValue { get; } = null;

    protected IList<Term> InnerTerms { get; set; } = new List<Term>();

    // Uses inner operations (or their calculated values / constants) to fill Value property.
    public abstract decimal GetResult();
}
