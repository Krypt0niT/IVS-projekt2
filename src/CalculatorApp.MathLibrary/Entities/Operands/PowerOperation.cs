namespace CalculatorApp.MathLibrary.Entities.Operands;

public class PowerOperation : Term
{
    public uint Exponent { get; init; }

    public PowerOperation(uint exponent, IList<Term> innerTerms) : base(innerTerms)
    {
        Exponent = exponent;
    }

    public override decimal GetResult()
    {
        if (InnerTerms!.Count == 0)
            return 0;

        if (InnerTerms.Count != 1)
            throw new NotSupportedException();

        decimal baseValue = InnerTerms.First().GetResult();

        return DecimalPow(baseValue, Exponent);
    }

    private decimal DecimalPow(decimal baseValue, uint exponent)
    {
        decimal result = 1;

        for (uint i = 0; i < exponent; i++)
        {
            result *= baseValue;
        }

        return result;
    }
}
