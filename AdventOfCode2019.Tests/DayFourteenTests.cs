using AdventOfCode2019.Fourteen;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayFourteenTests
    {
        [Theory]
        [InlineData(@"Fourteen\DayFourteenTestInputA.txt", 165)]
        //[InlineData(@"Fourteen\DayFourteenTestInputB.txt", 13312)]
        public void CalculateOreRequired(string filePath, int expected)
        {
            var sut = new DayFourteen();
            var result = sut.CalculateOreRequired(filePath);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayFourteen();
            var result = sut.PartA();

            Assert.Equal("9958", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayFourteen();
            var result = sut.PartB();

            Assert.Equal("1110", result);
        }
    }
}