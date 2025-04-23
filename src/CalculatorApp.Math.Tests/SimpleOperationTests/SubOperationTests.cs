using CalculatorApp.MathLibrary.Entities;
using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.SimpleOperationTests;
    public class SubOperationTests
    {
        [Fact]
        public void SingleSubTest()
        {
            var input = new SubOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(1),
                }
            );

            var result = input.GetResult();

            result.Should().Be(-1);
        }

        [Fact]
        public void MultipleSubTest()
        {
            var input = new SubOperation
            (
                new List<Term>
                {
                        new AbsoluteMember(1),
                        new AbsoluteMember(2),
                        new AbsoluteMember(-2),
                }
            );

            var result = input.GetResult();

            result.Should().Be(1);
        }

        [Fact]
        public void DepthSubTest()
        {
            var input = new SubOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(1),
                    new SubOperation
                    (
                        new List<Term>
                        {
                            new AbsoluteMember(5),
                            new AbsoluteMember(5),
                        }
                    ),
                    new AbsoluteMember(-15),
                    new AbsoluteMember(1),
                }
            );

            var result = input.GetResult();

            // 1 - 5 - 5 + 15 - 1
            result.Should().Be(15m);
        }

        [Fact]
        public void EmptySubTest()
        {
            var input = new SubOperation
            (
                new List<Term>
                {
                }
            );

            var result = input.GetResult();

            result.Should().Be(0);
        }

        [Fact]
        public void FloatingSubTest()
        {
            var input = new SubOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(0.1m),
                    new AbsoluteMember(0.2m),
                }
            );

            var result = input.GetResult();

            result.Should().Be(-0.1m);
        }
}

