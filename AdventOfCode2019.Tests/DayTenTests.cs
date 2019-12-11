using AdventOfCode2019.Ten;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayTenTests
    {
        [Theory]
        [InlineData(@"Ten\DayTenTestInputAA.txt", 8)]
        [InlineData(@"Ten\DayTenTestInputA.txt", 33)]
        [InlineData(@"Ten\DayTenTestInputB.txt", 35)]
        [InlineData(@"Ten\DayTenTestInputC.txt", 41)]
        [InlineData(@"Ten\DayTenTestInputD.txt", 210)]
        public void HowManyAsteroidsSeenFromBestLocation(string filePath, int expected)
        {
            var sut = new DayTen();
            var result = sut.HowManyAsteroidsSeenFromBestLocation(filePath);
            var numberSeen = result.Distances.Count;

            Assert.Equal(expected, numberSeen);
        }

        [Fact]
        public void RunAsteroidRoutine()
        {
            string filePath = @"Ten\DayTenTestInputE.txt";
            var sut = new DayTen();
            var result = sut.RunAsteroidRoutine(filePath, 9);

            Assert.Equal(901, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTen();
            var result = sut.PartA();

            Assert.Equal("263", result);
        }

        // 2926 is too high
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTen();
            var result = sut.PartB();

            Assert.Equal("1110", result);
        }
    }
}