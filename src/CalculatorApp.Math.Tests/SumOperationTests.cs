using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.Operands;
using FluentAssertions;

namespace CalculatorApp.Math.Tests;

public class SumOperationTests
{
    [Fact]
    public void SingleSumTest()
    {
        var input = new SumOperation
        (
            new List<Term>
            {
                new AbsoluteMember(1),
                new AbsoluteMember(2),
            }
        );

        var result = input.GetResult();

        result.Should().Be(3m);
    }

    [Fact]
    public void DepthSumTest()
    {
        var input = new SumOperation
        (
            new List<Term>
            {
                new AbsoluteMember(1),
                new SumOperation
                (
                    new List<Term>
                    {
                        new AbsoluteMember(5),
                        new AbsoluteMember(5),
                    }
                ),
                new AbsoluteMember(15),
                new AbsoluteMember(1),

            }
        );

        var result = input.GetResult();

        result.Should().Be(27m);
    }
}