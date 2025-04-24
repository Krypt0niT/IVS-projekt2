using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.SimpleOperationTests;
    public class MulOperationTests
    {
        [Fact]
        public void SingleMulTest()
        {
            var input = new MulOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(1),
                }
            );

            var result = input.GetResult();

            result.Should().Be(1); // * 1 = 1
        }

        [Fact]
        public void MultipleMulTest()
        {
            var input = new MulOperation
            (
                new List<Term>
                {
                        new AbsoluteMember(1),
                        new AbsoluteMember(2),
                        new AbsoluteMember(-2),
                }
            );

            var result = input.GetResult();

            result.Should().Be(-4m); // 1 * 2 * (-2) = -4
        }

        [Fact]
        public void DepthMulTest()
        {
            var input = new MulOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(1),
                    new MulOperation
                    (
                        new List<Term>
                        {
                            new AbsoluteMember(5),
                            new AbsoluteMember(5),
                        }
                    ),
                    new AbsoluteMember(-5),
                    new AbsoluteMember(-1),
                }
            );

            var result = input.GetResult();

            result.Should().Be(125m); // 1 * (5 * 5) * (-5) * (-1) = 125
        }

        [Fact]
        public void EmptyMulTest()
        {
            var input = new MulOperation
            (
                new List<Term>
                {
                }
            );

            var result = input.GetResult();

            result.Should().Be(0);
        }

        [Fact]
        public void FloatingMulTest()
        {
            var input = new MulOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(0.1m),
                    new AbsoluteMember(0.2m),
                }
            );

            var result = input.GetResult();

            result.Should().Be(0.02m); // 0.1 * 0.2 = 0.02
        }
    }

