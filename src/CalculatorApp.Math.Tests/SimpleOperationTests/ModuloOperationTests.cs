using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.SimpleOperationTests;
    public class ModuloOperationTests
    {
    [Fact]
    public void SingleModuloTest()
    {
        var input = new ModuloOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(10),
            }
        );

        var result = input.GetResult();

        result.Should().Be(10);
    }

    [Fact]
    public void MultipleModuloErrorTest()
    {
        var input = new ModuloOperation
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
    public void DepthModuloTest()
    {
        var input = new ModuloOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(4),
                    new ModuloOperation
                    (
                        new List<Term>
                        {
                            new AbsoluteMember(7),
                            new AbsoluteMember(5),
                        }
                    ),
            }
        );

        var result = input.GetResult();

        result.Should().Be(0m);
    }

    [Fact]
    public void EmptyModuloTest()
    {
        var input = new ModuloOperation
        (
            new List<Term>
            {
            }
        );

        var result = input.GetResult();

        result.Should().Be(0);
    }

    [Fact]
    public void FloatingModuloTest()
    {
        var input = new ModuloOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(0.5m),
                    new AbsoluteMember(0.2m)
            }
        );

        var result = input.GetResult();

        result.Should().Be(0.1m);
    }
}

