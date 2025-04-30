using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.LateXStyles;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.LateXFormatTests;
    public class MulFormatTests
    {
    [Fact]
    public void SimpleMulTest()
    {
        var input = new MulOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(12),
                    new AbsoluteMember(10)
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"12 \times 10";

        result.Should().Be(expectedLateX);  // 12 × 10 Should be all black
    }

    [Fact]
    public void SelectedFullMulTest()
    {
        var input = new MulOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(12),
                    new AbsoluteMember(10)
            }
        );
        input.IsSelected = true;


        var result = input.GetLateX();

        var expectedLateX = @"12 \colorbox{red}{\times} 10";

        result.Should().Be(expectedLateX); //  Only × should be red
    }

}

