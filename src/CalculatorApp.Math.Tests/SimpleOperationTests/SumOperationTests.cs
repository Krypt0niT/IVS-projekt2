using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.SimpleOperationTests;

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

        result.Should().Be(3.0m);
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

        // 1 + 5 + 5 + 15 + 1
        result.Should().Be(27m);
    }

    [Fact]
    public void EmptySumTest()
    {
        var input = new SumOperation
        (
            new List<Term>
            {
            }
        );

        var result = input.GetResult();

        result.Should().Be(0);
    }

    [Fact]
    public void FloatingErrorSumTest()
    {
        var input = new SumOperation
        (
            new List<Term>
            {
                new AbsoluteMember(0.1m),
                new AbsoluteMember(0.2m),
            }
        );

        var result = input.GetResult();

        result.Should().Be(0.3m);
    }
}