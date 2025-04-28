using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.LateXStyles;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.LateXFormatTests;
    public class DivFormatTests
    {
    [Fact]
    public void SimpleDivTest()
    {
        var input = new AbsOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(-10),
                    new AbsoluteMember(10)
            }
        );

        var result = input.GetLateX();

        result.Should().Be("\frac{-10}{10}"); // -10/10
    }

    [Fact]
    public void SelectedFullDivTest()
    {
        var input = new AbsOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(-10),
                    new AbsoluteMember(10)
            }
        );
        input.IsSelected = true;


        var result = input.GetLateX();

        var expectedLateX = @"\color{red}{\frac{-10}{10}}";

        result.Should().Be(expectedLateX); //  Fraction should be fully red
    }

    [Fact]
    public void SelectedNumeratorTest() // selected 'citatel'
    {
        var input = new AbsOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(-10)
                    {
                        IsSelected = true
                    },
                    new AbsoluteMember(10)
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"\frac{\color{red}{-10}}{10}";

        result.Should().Be(expectedLateX); // Only numerator should be red
    }

    public void SelectedDenominatorTest() // selected 'menovatel'
    {
        var input = new AbsOperation
        (
            new List<Term>
            {
                    new AbsoluteMember(-10)
                    {
                        IsSelected = true
                    },
                    new AbsoluteMember(10)
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"\frac{-10}{\color{red}{10}}";

        result.Should().Be(expectedLateX); // Only denominator should be red
    }
}

