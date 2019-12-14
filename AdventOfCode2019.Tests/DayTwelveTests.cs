using AdventOfCode2019.Twelve;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayTwelveTests
    {
        [Theory]
        [InlineData(@"Twelve\DayTwelveTestInputA.txt", 10, 179)]
        [InlineData(@"Twelve\DayTwelveTestInputB.txt", 100, 1940)]
        public void HowManyAsteroidsSeenFromBestLocation(string filePath, int timeSteps, int expected)
        {
            var sut = new DayTwelve();
            var result = sut.CalculateTotalSystemEnergy(filePath, timeSteps);

            Assert.Equal(expected, result);
        }

        // TODO: Come back to, as runs forever now
        /*
        [Theory]
        [InlineData(@"Twelve\DayTwelveTestInputA.txt", 2772)]
        //[InlineData(@"Twelve\DayTwelveTestInputB.txt", 4686774924)]
        public void StepsUntilPositionsRepeated(string filePath, long expected)
        {
            var sut = new DayTwelve();
            var result = sut.StepsUntilPositionsRepeated(filePath);

            Assert.Equal(expected, result);
        }
        */

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwelve();
            var result = sut.PartA();

            Assert.Equal("9958", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwelve();
            var result = sut.PartB();

            Assert.Equal("1110", result);
        }
    }
}