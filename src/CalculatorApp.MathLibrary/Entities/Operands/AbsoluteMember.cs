namespace CalculatorApp.MathLibrary.Entities.Operands;

public class AbsoluteMember : Term
{
    public AbsoluteMember(decimal absoluteValue) : base(absoluteValue)
    {
    }

    public override decimal GetResult()
    {
        return AbsoluteValue!.Value;
    }
}
