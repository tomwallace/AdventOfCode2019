using AdventOfCode2019.Five;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayFiveTests
    {
        // 1100 is too low
        // 2717 is too low
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayFive();
            var result = sut.PartA();

            Assert.Equal("1855", result);
        }

        // 1042 is too low
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayFive();
            var result = sut.PartB();

            Assert.Equal("1253", result);
        }
    }
}