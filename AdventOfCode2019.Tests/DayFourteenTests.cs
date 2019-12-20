using AdventOfCode2019.Fourteen;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayFourteenTests
    {
        [Theory]
        [InlineData(@"Fourteen\DayFourteenTestInputA.txt", 165)]
        [InlineData(@"Fourteen\DayFourteenTestInputB.txt", 13312)]
        [InlineData(@"Fourteen\DayFourteenTestInputC.txt", 180697)]
        [InlineData(@"Fourteen\DayFourteenTestInputD.txt", 2210736)]
        public void CalculateOreRequired(string filePath, int expected)
        {
            var sut = new DayFourteen();
            var result = sut.CalculateOreRequired(filePath, 1);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(@"Fourteen\DayFourteenTestInputB.txt", 82892753)]
        [InlineData(@"Fourteen\DayFourteenTestInputC.txt", 5586022)]
        [InlineData(@"Fourteen\DayFourteenTestInputD.txt", 460664)]
        public void AmountFuelProduced(string filePath, int expected)
        {
            var sut = new DayFourteen();
            var result = sut.AmountFuelProduced(filePath);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayFourteen();
            var result = sut.PartA();

            Assert.Equal("2486514", result);
        }

        // 2485514 is too high
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayFourteen();
            var result = sut.PartB();

            Assert.Equal("998536", result);
        }
    }
}