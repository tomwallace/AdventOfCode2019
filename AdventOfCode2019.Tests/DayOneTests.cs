using AdventOfCode2019.One;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayOneTests
    {
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void CalculateFuelRequired(int mass, int expected)
        {
            var sut = new DayOne();
            var result = sut.CalculateFuelRequired(mass);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void CalculateFuelAccountingForFuelWeight(int mass, int expected)
        {
            var sut = new DayOne();
            var result = sut.CalculateFuelAccountingForFuelWeight(mass);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayOne();
            var result = sut.PartA();

            Assert.Equal("3550236", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayOne();
            var result = sut.PartB();

            Assert.Equal("5322455", result);
        }
    }
}