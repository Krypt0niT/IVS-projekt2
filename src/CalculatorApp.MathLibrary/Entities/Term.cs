namespace CalculatorApp.MathLibrary.Entities;

public abstract class Term
{
    public Term(IList<Term>? innerTerms)
    {
        InnerTerms = innerTerms;
    }

    public IList<Term>? InnerTerms { get; set; }

    // TODO: inkapsulizovat?
    public bool IsSelected { get; set; }

    // Uses inner operations (or their calculated values / constants) to fill Value property.
    public abstract decimal GetResult();
    public abstract string GetLateX();
}
