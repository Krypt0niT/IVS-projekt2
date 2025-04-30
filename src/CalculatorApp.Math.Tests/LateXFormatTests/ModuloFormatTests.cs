using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.LateXStyles;
using FluentAssertions;


namespace CalculatorApp.Math.Tests.LateXFormatTests;
    public class ModuloFormatTests
    {
    [Fact]
    public void SimpleModuloTest()
    {
        var input = new ModuloOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(12),
                    new AbsoluteMember(10)
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"12 \% 10";

        result.Should().Be(expectedLateX);  // 12 % 10 Should be all black
    }

    [Fact]
    public void SelectedFullModuloTest()
    {
        var input = new ModuloOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(12),
                    new AbsoluteMember(10)
            }
        );
        input.IsSelected = true;


        var result = input.GetLateX();

        var expectedLateX = @"12 \colorbox{red}{\%} 10";

        result.Should().Be(expectedLateX); //  Only % should be red
    }

}

