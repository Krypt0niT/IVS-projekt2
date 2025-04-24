using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.SimpleOperationTests;
    public class AbsOperationTests
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

            var result = input.GetResult();

            result.Should().Be(10); // |-10| = 10
        }
        [Fact]
        public void SinglePositiveAbsTest()
        {
            var input = new AbsOperation
            (
                new List<Term>
                {
                        new AbsoluteMember(10),
                }
            );

            var result = input.GetResult();

            result.Should().Be(10); // |10| = 10
        }

        [Fact]
            public void MultipleAbsErrorTest()
            {
                var input = new AbsOperation
                (
                    new List<Term>
                    {
                        new AbsoluteMember(10),
                        new AbsoluteMember(-15),
                        new AbsoluteMember(1),
                    }
                );

            Assert.Throws<NotSupportedException>(() =>
            {
                var result = input.GetResult();
            });
        }

        [Fact]
        public void DepthAbsTest()
        {
            var input = new AbsOperation
            (
                new List<Term>
                {
                    new AbsOperation
                    (
                        new List<Term>
                        {
                            new AbsoluteMember(-5),
                        }
                    ),
                }
            );

            var result = input.GetResult();

            result.Should().Be(5m); // ||-5|| = 5
        }

        [Fact]
        public void EmptyAbsTest()
        {
            var input = new AbsOperation
            (
                new List<Term>
                {
                }
            );

            var result = input.GetResult();

            result.Should().Be(0);
        }

        [Fact]
        public void FloatingAbsTest()
        {
            var input = new AbsOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(-0.2m)
                }
            );

            var result = input.GetResult();

            result.Should().Be(0.2m); // |-0.2| = 0.2
        }
    }


