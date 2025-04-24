using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.SimpleOperationTests;
    public class FactorialOperationTests
    {
    [Fact]
    public void SingleFactorialTest()
    {
        var input = new FactorialOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(5),
            }
        );

        var result = input.GetResult();

        result.Should().Be(120);
    }

    [Fact]
    public void MultipleFactorialErrorTest()
    {
        var input = new FactorialOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(10),
                    new AbsoluteMember(15),
                    new AbsoluteMember(1),
            }
        );

        Assert.Throws<NotSupportedException>(() =>
        {
            var result = input.GetResult();
        });

    }


    [Fact]
    public void DepthFactorialTest()
    {
        var input = new FactorialOperation
        (
            new List<Term>
            {
                    new FactorialOperation
                    (
                        new List<Term>
                        {
                            new AbsoluteMember(3),
                        }
                    ),
            }
        );

        var result = input.GetResult();

        result.Should().Be(720m);
    }

    [Fact]
    public void EmptyFactorialTest()
    {
        var input = new FactorialOperation
        (
            new List<Term>
            {
            }
        );

        var result = input.GetResult();

        result.Should().Be(0);
    }

    [Fact]
    public void FloatingFactorialErrorTest()
    {
        var input = new FactorialOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(0.5m),
            }
        );

        Assert.Throws<NotSupportedException>(() =>
        {
            var result = input.GetResult();
        });
    }
}

