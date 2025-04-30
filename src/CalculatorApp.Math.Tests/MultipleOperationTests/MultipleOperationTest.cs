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
            new AbsoluteMember(5),
            new AbsoluteMember(8)
           }
        );  // 5 * 8 = 40

        var mul2 = new MulOperation
        (
           new List<Term>
           {
            new AbsoluteMember(4),
            new AbsoluteMember(5)
           }
        );  // 4 * 5 = 20

        var factorial = new FactorialOperation
        (
           new List<Term>
           {
            new AbsoluteMember(4)
           }
        );  // 4! = 24

        var div = new DivOperation
        (
           new List<Term>
           {
            factorial,
            mul2
           }
        ); // 24/20 = 6/5 = 1.2

        var sum = new SumOperation(new List<Term>{
        mul1,
        div
    });  // 40 + 1.2 = 41.2

        var power = new PowerOperation(new AbsoluteMember(2),
            new List<Term>{
            sum
            }
        );  // 41.2^2 = 1697.44

        var result = power.GetResult();
        result.Should().Be(1697.44m);  // ((5 * 8)+(4! /(4 * 5)))^2
    }

    [Fact]
    public void ComplexTest2()
    {
        var factorial = new FactorialOperation(
            new List<Term> { new AbsoluteMember(4) }  // 4! = 24
        );

        var root = new NthRootOperation(new AbsoluteMember(3),
            new List<Term> { new AbsoluteMember(27) }  // ∛27 = 3
        );

        var sum = new SumOperation(
            new List<Term> { factorial, root }  // 24 + 3 = 27
        );

        var power = new PowerOperation(new AbsoluteMember(2),
            new List<Term> { sum }  // 27^2 = 729
        );

        var result = power.GetResult();
        result.Should().Be(729m);  // (4! + ∛27)^2 = 729
    }

    [Fact]
    public void ComplexTest3()
    {
        var abs = new AbsOperation(
            new List<Term> { new AbsoluteMember(-15) }  // |-15| = 15
        );

        var modulo = new ModuloOperation(
            new List<Term> {
            new AbsoluteMember(23),
            new AbsoluteMember(7)
            }
        );  // 23 % 7 = 2

        var mul = new MulOperation(
            new List<Term> { abs, modulo }
        );  // 15 * 2 = 30

        var sub = new SubOperation(
            new List<Term> {
            mul
            }
        );  // - 30

        var sum = new SumOperation(
            new List<Term> {
            new AbsoluteMember(100),
            sub
            }
        );  // 100 + (-30) = 70

        var result = sum.GetResult();
        result.Should().Be(70m);  // 100 - (|-15| * (23 % 7)) = 70
    }
}

