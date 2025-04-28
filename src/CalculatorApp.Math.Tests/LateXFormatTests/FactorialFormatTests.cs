using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.LateXStyles;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.LateXFormatTests;
    public class FactorialFormatTests
    {
    [Fact]
    public void SimpleFactorialTest()
    {
        var input = new FactorialOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(5),
            }
        );

        var result = input.GetLateX();

        result.Should().Be("5!"); // 5! Should be all black
    }

    [Fact]
    public void SelectedFactorialTest()
    {
        var input = new FactorialOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(5)
            }
        );
        input.IsSelected = true;


        var result = input.GetLateX();

        var expectedLateX = @"5\colorbox{red}{!}";

        result.Should().Be(expectedLateX); // Only ! should be red
    }

    [Fact]
    public void SelectedAbsoluteMemberTest()
    {
        var input = new FactorialOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(5)
                    {
                        IsSelected = true
                    }
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"\colorbox{red}{5}!";

        result.Should().Be(expectedLateX); // Only number should be red
    }
}

