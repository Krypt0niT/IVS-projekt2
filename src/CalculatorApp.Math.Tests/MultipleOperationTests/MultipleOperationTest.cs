using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.MultipleOperationTests;
    public class MultipleOperationTest
    {
    [Fact]
    public void ComplexTest1()
    {
        var mul1 = new MulOperation
        (
           new List<Term>
           {
                new AbsoluteMember(6),
                new AbsoluteMember(7)
           }
        );

        var mul2 = new MulOperation
        (
           new List<Term>
           {
                new AbsoluteMember(7),
                new AbsoluteMember(2)
           }
        );

        var factorial = new FactorialOperation
        (
           new List<Term>
           {
                new AbsoluteMember(3)
           }
        );

        var div = new DivOperation
        (
           new List<Term>
           { factorial 
                factorial,
                mul2
           }
        );

        var sum = new SumOperation(new List<Term>{ 
            mul1,
            div
        });

        var power = new PowerOperation(2,
            new List<Term>{
                sum
            }
        );

        var result = power.GetResult(); // (6 * 7 + 3!/(7*2))^(2)
        result.Should().Be(1800.1836734693877551020408164m);
    }
}

