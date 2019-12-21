using AdventOfCode2019.Twenty;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayTwentyTests
    {
        [Theory]
        [InlineData(@"Twenty\DayTwentyTestInputA.txt", 23)]
        [InlineData(@"Twenty\DayTwentyTestInputB.txt", 58)]
        public void FindFewestStepsInMaze(string filePath, int expected)
        {
            var sut = new DayTwenty();
            var result = sut.FindFewestStepsInMaze(filePath);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwenty();
            var result = sut.PartA();

            Assert.Equal("9958", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwenty();
            var result = sut.PartB();

            Assert.Equal("1110", result);
        }
    }
}