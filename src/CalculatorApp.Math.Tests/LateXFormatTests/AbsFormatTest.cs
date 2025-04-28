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

            var result = input.GetLateX();

            result.Should().Be("|-10|"); // |-10|
        }

        [Fact]
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

            var expectedLateX = @"\colorbox{red}{|}-10\colorbox{red}{|}";

            result.Should().Be(expectedLateX); // |-10|
        }

        [Fact]
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

            var expectedLateX = @"|\colorbox{red}{-10}|";

            result.Should().Be(expectedLateX); // |-10|
        }
    }
}
