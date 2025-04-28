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

        var input = new NthRootOperation(4,
            new List<Term>{
                new AbsoluteMember(16)
            }
        );

        var result = input.GetResult();

        result.Should().Be(2m); // √16 = 2
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
    public void PerfectCubeRootTest()
    {
        var input = new NthRootOperation(3,
            new List<Term>{
            new AbsoluteMember(-64)
            }
        );

        var result = input.GetResult();
        result.Should().Be(-4m); // ∛64 = 4
    }

    [Fact]
    public void DepthNthRootTest()
    {
        var inner = new NthRootOperation(2,
            new List<Term>
            {
            new AbsoluteMember(64)
            }
        );  // √(64) = 8

        var outer = new NthRootOperation(3,
            new List<Term>
            {
            inner
            }
        );  // ∛(8) = 2

        var result = outer.GetResult();

        result.Should().Be(2m);
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
                new AbsoluteMember(0.25m)
            }
        );

        var result = input.GetResult();

        result.Should().Be(0.5m); // √(0.25) = 0.5
    }

    [Fact]
    public void NegativeNthRootErrorTest()
    {
        var input = new NthRootOperation(-3,
            new List<Term>{
                new AbsoluteMember(27)
            }
        );

        Assert.Throws<NotSupportedException>(() =>
        {
            var result = input.GetResult();     // (-3)rd-root(5)
        });

    }

    [Fact]
    public void NegativeNumUnderEvenRootErrorTest()
    {
        var input = new NthRootOperation(4,
            new List<Term>{
                new AbsoluteMember(-5)
            }
        );

        Assert.Throws<NotSupportedException>(() =>
        {
            var result = input.GetResult();     // 4th-root(-5)
        });

    }
}

