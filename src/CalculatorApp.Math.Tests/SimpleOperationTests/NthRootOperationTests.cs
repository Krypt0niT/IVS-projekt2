using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.SimpleOperationTests;
    public class NthRootOperationTests
    {

    [Fact]
    public void SingleNthRootOperationTest()
    {

        var input = new NthRootOperation(2,
            new List<Term>{
                new AbsoluteMember(3)
            }
        );

        var result = input.GetResult();

        result.Should().Be(1.732050807568877295254353946m);
    }

    [Fact]
    public void MultipleNthRootErrorTest()
    {
        var input = new NthRootOperation(3,
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
    public void DepthNthRootTest()
    {
        var inner = new NthRootOperation(3,
            new List<Term>
            {
            new AbsoluteMember(8)
            }
        );

        var outer = new NthRootOperation(2,
            new List<Term>
            {
            inner
            }
        );

        var result = outer.GetResult();

        result.Should().Be(1.4142135623730950492251214604m);
    }

    [Fact]
    public void EmptyNthRootTest()
    {
        var input = new NthRootOperation(3,
            new List<Term>
            {

            }
        );

        var result = input.GetResult();

        result.Should().Be(0);
    }

    [Fact]
    public void FloatingNthRootTest()
    {
        var input = new NthRootOperation(2,
            new List<Term>{
                new AbsoluteMember(0.2m)
            }
        );

        var result = input.GetResult();

        result.Should().Be(0.4472135955001610322835205142m);

    }
}

