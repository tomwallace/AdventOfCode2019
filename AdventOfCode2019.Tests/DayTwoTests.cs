using AdventOfCode2019.Two;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayTwoTests
    {
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwo();
            var result = sut.PartA();

            Assert.Equal("3085697", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwo();
            var result = sut.PartB();

            Assert.Equal("9425", result);
        }
    }
}