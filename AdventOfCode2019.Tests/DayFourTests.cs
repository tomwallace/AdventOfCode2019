using AdventOfCode2019.Four;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayFourTests
    {
        [Theory]
        [InlineData(111111, 111111, 1)]
        [InlineData(223450, 223450, 0)]
        [InlineData(123789, 123789, 0)]
        public void NumberMatchingPasswords_PartARules(int inputLower, int inputUpper, int expected)
        {
            List<IPasswordRule> rules = new List<IPasswordRule>()
            {
                new CorrectDigits(), new AdjacentDigitsDoNotDecrease(), new DuplicateDigit()
            };

            var sut = new DayFour();
            var result = sut.NumberMatchingPasswords(inputLower, inputUpper, rules);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(112233, 112233, 1)]
        [InlineData(123444, 123444, 0)]
        [InlineData(111122, 111122, 1)]
        public void NumberMatchingPasswords_PartBRules(int inputLower, int inputUpper, int expected)
        {
            List<IPasswordRule> rules = new List<IPasswordRule>()
            {
                new CorrectDigits(), new AdjacentDigitsDoNotDecrease(), new DuplicateDigitExcludeTriple()
            };

            var sut = new DayFour();
            var result = sut.NumberMatchingPasswords(inputLower, inputUpper, rules);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayFour();
            var result = sut.PartA();

            Assert.Equal("1855", result);
        }

        // 1042 is too low
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayFour();
            var result = sut.PartB();

            Assert.Equal("1253", result);
        }
    }
}