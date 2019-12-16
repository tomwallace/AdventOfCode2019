using AdventOfCode2019.Fifteen;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayFifteenTests
    {
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayFifteen();
            var result = sut.PartA();

            Assert.Equal("304", result);
        }

        // 10 is too low
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayFifteen();
            var result = sut.PartB();

            Assert.Equal("310", result);
        }
    }
}