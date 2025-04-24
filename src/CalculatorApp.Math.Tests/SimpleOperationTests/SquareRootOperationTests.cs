using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.SimpleOperationTests;
    public class SquareRootOperationTests
    {

    [Fact]
    public void SingleSquareRootOperationTest()
    {

        var input = new SquareRootOperation(2,
            new List<Term>{
                new AbsoluteMember(3)
            }
        );

        var result = input.GetResult();

        result.Should().Be(1.732050807568877295254353946m);
    }

    [Fact]
    public void MultipleSquareRootErrorTest()
    {
        var input = new SquareRootOperation(3,
            new List<Term>{
                new AbsoluteMember(5),
                new AbsoluteMember(5)
            }
        );

        Assert.Throws<NotSupportedException>(() =>
        {
            var result = input.GetResult();
        });

    }


    [Fact]
    public void DepthSquareRootTest()
    {
        var inner = new SquareRootOperation(3,
            new List<Term>
            {
            new AbsoluteMember(8)
            }
        );

        var outer = new SquareRootOperation(2,
            new List<Term>
            {
            inner
            }
        );

        var result = outer.GetResult();

        result.Should().Be(1.4142135623730950492251214604m);
    }

    [Fact]
    public void EmptySquareRootTest()
    {
        var input = new SquareRootOperation(3,
            new List<Term>
            {

            }
        );

        var result = input.GetResult();

        result.Should().Be(0);
    }

    [Fact]
    public void FloatingSquareRootTest()
    {
        var input = new SquareRootOperation(2,
            new List<Term>{
                new AbsoluteMember(0.2m)
            }
        );

        var result = input.GetResult();

        result.Should().Be(0.4472135955001610322835205142m);

    }
}

