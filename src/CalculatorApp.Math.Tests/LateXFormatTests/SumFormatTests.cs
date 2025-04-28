using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.LateXStyles;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.LateXFormatTests;
    public class SumFormatTests
    {
    [Fact]
    public void SimpleSubTest()
    {
        var input = new SubOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(12),
                    new AbsoluteMember(10)
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"12 + 10";

        result.Should().Be(expectedLateX);  // 12 + 10 Should be all black
    }

    [Fact]
    public void SelectedFullSubTest()
    {
        var input = new SubOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(12),
                    new AbsoluteMember(10)
            }
        );
        input.IsSelected = true;


        var result = input.GetLateX();

        var expectedLateX = @"12 \colorbox{red}{+} 10";

        result.Should().Be(expectedLateX); //  Only + should be red
    }

    [Fact]
    public void SelectedLeftMemberTest()
    {
        var input = new SubOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(12)
                    {
                        IsSelected = true
                    },
                    new AbsoluteMember(10)
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"\colorbox{red}{12} + 10";

        result.Should().Be(expectedLateX); // Only left member should be red
    }

    public void SelectedRightMemberTest()
    {
        var input = new SubOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(12),

                    new AbsoluteMember(10){
                        IsSelected = true
                    }
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"12 + \colorbox{red}{10}";

        result.Should().Be(expectedLateX); // Only right member should be red
    }
}
