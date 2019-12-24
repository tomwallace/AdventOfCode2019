using AdventOfCode2019.TwentyFour;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayTwentyFourTests
    {
        [Theory]
        [InlineData(@"TwentyFour\DayTwentyFourTestInputA.txt", 2129920)]
        public void BioDiversityAfterFirstRepeat(string filePath, double expected)
        {
            var sut = new DayTwentyFour();
            var result = sut.BioDiversityAfterFirstRepeat(filePath);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwentyFour();
            var result = sut.PartA();

            Assert.Equal("28772955", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwentyFour();
            var result = sut.PartB();

            Assert.Equal("-1", result);
        }
    }
}