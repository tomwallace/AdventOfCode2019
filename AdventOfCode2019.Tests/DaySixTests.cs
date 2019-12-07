using AdventOfCode2019.Six;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DaySixTests
    {
        [Fact]
        public void CalculateNumberOfOrbits()
        {
            string filePath = @"Six\DaySixTestInput.txt";
            var sut = new DaySix();
            var result = sut.CalculateNumberOfOrbits(filePath);

            Assert.Equal(42, result);
        }

        [Fact]
        public void CalculateOrbitsBetweenYouAndSanta()
        {
            string filePath = @"Six\DaySixTestInput2.txt";
            var sut = new DaySix();
            var result = sut.CalculateOrbitsBetweenYouAndSanta(filePath);

            Assert.Equal(4, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DaySix();
            var result = sut.PartA();

            Assert.Equal("117672", result);
        }

        // 1042 is too low
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DaySix();
            var result = sut.PartB();

            Assert.Equal("277", result);
        }
    }
}