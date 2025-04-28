namespace CalculatorApp.MathLibrary.Entities.Operands;
    /// <summary>
    /// Represents a subtraction operation (a - b - c - ...).
    /// </summary>
    /// <remarks>
    /// This class performs subtraction by taking the first term and subtracting all subsequent terms in the list.
    /// If only one term is provided, it returns its negation.
    /// </remarks>
    public class SubOperation : Term
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubOperation"/> class.
        /// </summary>
        /// <param name="innerTerms">The list of terms to be subtracted.</param>
        public SubOperation(IList<Term> innerTerms) : base(innerTerms)
        {
        }

    public override string GetLateX()
    {
        var result = "";
        if (InnerTerms == null) throw new Exception();
        if (InnerTerms.Count == 1) return InnerTerms.First().GetLateX();

        foreach (var a in InnerTerms)
        {
            result += " - " + a.GetLateX();
        }

        result = result.Trim();

        return result;
    }

    /// <summary>
    /// Computes the result of the subtraction operation.
    /// </summary>
    /// <returns>The result of subtracting all inner terms.</returns>
    public override decimal GetResult()
        {
            decimal result = 0;
            if (InnerTerms.Count == 0) return 0;

            for (int i = 0; i < InnerTerms.Count; i++)
            {
                result -= InnerTerms[i].GetResult();
            }

            return result;
        }
    }

