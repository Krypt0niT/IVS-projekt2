using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.LateXFormatTests;
public class NthRootFormatTests
{
    [Fact]
    public void NthRootOperationTest()
    {
        var input = new NthRootOperation(new AbsoluteMember(3),
            new List<Term>{
                new AbsoluteMember(27)
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"\sqrt[3]{27}";

        result.Should().Be(expectedLateX);  // ³√27 Should be all black 
    }

    [Fact]
    public void SelectedDegreeNthRootOperationTest()
    {
        var input = new NthRootOperation(new AbsoluteMember(3),
            new List<Term>{
                new AbsoluteMember(27)
            }
        );
        input.IsSelected = true;

        var result = input.GetLateX();

        var expectedLateX = @"\sqrt[\colorbox{red}{3}]{27}";

        result.Should().Be(expectedLateX);  // Only degree should be red 
    }

    [Fact]
    public void SelectedAbsoluteMemberNthRootOperationTest()
    {
        var input = new NthRootOperation(new AbsoluteMember(3),
            new List<Term>{
                new AbsoluteMember(27)
                { 
                    IsSelected = true
                }
            }
        );

        var result = input.GetLateX();

        var expectedLateX = @"\sqrt[3]{\colorbox{red}{27}}";

        result.Should().Be(expectedLateX);  // Only absolute member should be red 
    }
}

