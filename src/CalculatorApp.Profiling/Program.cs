using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities;

var input = Console.In.ReadToEnd();
var tokens = input.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
var numbers = new List<decimal>();

foreach (var token in tokens)
{
    if (decimal.TryParse(token, out var num))
        numbers.Add(num);
}

int N = numbers.Count;
if (N < 2)
{
    Console.WriteLine("Potrebujem aspoň 2 čísla.");
    return;
}

// SUM xi
var sum = new SumOperation(numbers.Select(n => (Term)new AbsoluteMember(n)).ToList()).GetResult();

// MEAN = sum / N
var mean = new DivOperation(new List<Term>
        {
            new AbsoluteMember(sum),
            new AbsoluteMember(N)
        }).GetResult();

// SUM xi^2
var sumSquares = new SumOperation(numbers.Select(n =>
    (Term)new PowerOperation(new AbsoluteMember(2), new List<Term> { new AbsoluteMember(n) })).ToList()).GetResult();

// N * mean^2
var meanSquared = new PowerOperation(new AbsoluteMember(2), new List<Term> { new AbsoluteMember(mean) }).GetResult();
var nMeanSquared = new MulOperation(new List<Term>
        {
            new AbsoluteMember(N),
            new AbsoluteMember(meanSquared)
        }).GetResult();

// Numerator = sum(xi^2) - N * mean^2
var numerator = new SubOperation(new List<Term>
        {
            new AbsoluteMember(-sumSquares),
            new AbsoluteMember(nMeanSquared)
        }).GetResult();

// Variance = numerator / (N - 1)
var variance = new DivOperation(new List<Term>
        {
            new AbsoluteMember(numerator),
            new AbsoluteMember(N - 1)
        }).GetResult();

var profiling = new NthRootOperation(new AbsoluteMember(2), new List<Term> { new AbsoluteMember(variance) }).GetResult();

Console.WriteLine(profiling);