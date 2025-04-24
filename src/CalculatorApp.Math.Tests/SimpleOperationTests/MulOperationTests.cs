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

            result.Should().Be(1);
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

            result.Should().Be(-4m);
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

            result.Should().Be(125m);
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

            result.Should().Be(0.02m);
        }
    }

