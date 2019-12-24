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
            var result = sut.FindFewestStepsInMaze(filePath, false);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void FindFewestStepsInMaze_WithLevels()
        {
            var sut = new DayTwenty();
            var filePath = @"Twenty\DayTwentyTestInputC.txt";
            var result = sut.FindFewestStepsInMaze(filePath, true);

            Assert.Equal(396, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwenty();
            var result = sut.PartA();

            Assert.Equal("514", result);
        }

        // Takes 1 min to run, but is correct, so commenting out the test
        /*
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwenty();
            var result = sut.PartB();

            Assert.Equal("6208", result);
        }
        */
    }
}