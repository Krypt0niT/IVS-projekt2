using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.LateXFormatTests;
    public class PowerFormatTests
    {
    [Fact]
    public void PowerOperationTest()
    {

        var input = new PowerOperation(12,
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

        var input = new PowerOperation(12,
            new List<Term>{
                new AbsoluteMember(10)
            }
        );
        input.IsSelected = true;

        var result = input.GetLateX();

        var expectedLateX = @"10^{\colorbox{red}{12}}";

        result.Should().Be(expectedLateX);  // Only exponent should be red 
    }

    [Fact]
    public void SelectedBasePowerOperationTest()
    {

        var input = new PowerOperation(12,
            new List<Term>{
                new AbsoluteMember(10)
                {
                    IsSelected = true
                }
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"\colorbox{red}{10}^{12}";

        result.Should().Be(expectedLateX);  // Absolute member should be red 
    }
}
