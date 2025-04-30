using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.LateXStyles;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.LateXFormatTests;
    public class PowerFormatTests
    {
    [Fact]
    public void PowerOperationTest()
    {
        var input = new PowerOperation(new AbsoluteMember(12),
            new List<Term>{
                new AbsoluteMember(10)
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"10^{12}";

        result.Should().Be(expectedLateX);  // 10^(12) Should be all black 
    }

    [Fact]
    public void SelectedExponentPowerOperationTest()
    {

        var input = new PowerOperation(new AbsoluteMember(12),
            new List<Term>{
                new AbsoluteMember(10)
            }
        );
        input.IsSelected = true;

        var result = input.GetLateX();

        var expectedLateX = @"10^{\colorbox{red}{12}}";

        result.Should().Be(expectedLateX);  // Only exponent should be red 
    }

}
