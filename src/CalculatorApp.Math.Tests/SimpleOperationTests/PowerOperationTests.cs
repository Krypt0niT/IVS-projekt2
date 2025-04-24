using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.SimpleOperationTests;
    public class PowerOperationTests
    {
    [Fact]
    public void SinglePowerOperationTest(){

        var input = new PowerOperation(0, 
            new List<Term>{
                new AbsoluteMember(0)
            }
        );

        var result = input.GetResult();

        result.Should().Be(1); // 0^0 = 1
    }

    [Fact]
    public void MultiplePowerErrorTest()
    {
        var input = new PowerOperation(3,
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
    public void DepthPowerTest()
    {
        var inner = new PowerOperation(3, // 5^3 = 125
            new List<Term>
            {
            new AbsoluteMember(5)
            }
        );

        var outer = new PowerOperation(2, // (5^3)^2 = 125^2 = 15625
            new List<Term>
            {
            inner
            }
        );

        var result = outer.GetResult();

        result.Should().Be(15625m);
    }

    [Fact]
    public void EmptyPowerTest()
    {
        var input = new PowerOperation(3,
            new List<Term>{

            }
        );

        var result = input.GetResult();

        result.Should().Be(0);
    }

    [Fact]
    public void FloatingPowerTest()
    {
        var input = new PowerOperation(3,
            new List<Term>{
                new AbsoluteMember(0.2m)
            }
        );

        var result = input.GetResult();

        result.Should().Be(0.008m);

    }

}
