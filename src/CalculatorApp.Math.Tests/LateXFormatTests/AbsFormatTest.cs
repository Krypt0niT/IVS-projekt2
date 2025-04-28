using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.LateXStyles;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.LateXFormatTests
{
    public class AbsFormatTest
    {
        [Fact]
        public void SingleNegativeAbsTest()
        {
            var input = new AbsOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(-10),
                }
            );

            // TODO: implemetation of tests.
            //var result = input.GetLateX();

            //result.Should().Be("\\left|-10\\right|"); // |-10|
        }

        public void SelectedAbsTest()
        {
            var input = new AbsOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(-10)
                }
            );
            input.IsSelected = true;
            

            var result = input.GetLateX();

            var expectedLateX = @"\color{red}{|} -10  \color{red}{|}";

            result.Should().Be(expectedLateX); // |-10|
        }

        public void SelectedAbsoluteMemberTest()
        {
            var input = new AbsOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(-10)
                    {
                        IsSelected = true
                    }
                }
            );

            var result = input.GetLateX();

            var expectedLateX = @"|\color{red}{-10}|";

            result.Should().Be(expectedLateX); // |-10|
        }
    }
}
