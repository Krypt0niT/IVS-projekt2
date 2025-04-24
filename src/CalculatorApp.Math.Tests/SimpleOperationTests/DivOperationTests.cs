using CalculatorApp.MathLibrary.Entities.AbsoluteMembers;
using CalculatorApp.MathLibrary.Entities.Operands;
using CalculatorApp.MathLibrary.Entities;
using FluentAssertions;

namespace CalculatorApp.Math.Tests.SimpleOperationTests;
    public class DivOperationTests
    {
        [Fact]
        public void SingleDivTest()
        {
            var input = new DivOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(1),
                }
            );

            var result = input.GetResult();

            result.Should().Be(1); // [  / 1 ] = 1
        }

        [Fact]
        public void MultipleDivTest()
        {
            var input = new DivOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(1),
                    new AbsoluteMember(2),
                    new AbsoluteMember(-1),
                }
            );

            var result = input.GetResult();

            result.Should().Be(-0.5m); // [ 1 / 2 / (-1) ] = -0.5
        }

        [Fact]
        public void DepthDivTest()
        {
            var input = new DivOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(1),
                    new DivOperation
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

            result.Should().Be(0.2m); // [ 1 / (5 / 5) / (-5) / (-1) ] = 0.2
        }

        [Fact]
        public void EmptyDivTest()
        {
            var input = new DivOperation
            (
                new List<Term>
                {
                }
            );

            var result = input.GetResult();

            result.Should().Be(0);
        }

        [Fact]
        public void FloatingDivTest()
        {
            var input = new DivOperation
            (
                new List<Term>
                {
                    new AbsoluteMember(0.1m),
                    new AbsoluteMember(0.2m),
                }
            );

            var result = input.GetResult();

            result.Should().Be(0.5m); // [ 0.1 / 0.2 ] = 0.5
        }

        [Fact]
        public void ZeroDivOperationErrorTest()
        {

            var input = new DivOperation
            (
               new List<Term>
               {
                    new AbsoluteMember(5),
                    new AbsoluteMember(0),
               }
            );

            Assert.Throws<DivideByZeroException>(() =>
            {
                var result = input.GetResult(); // [ 5 / 0 ] = error
            });
        }
    }

