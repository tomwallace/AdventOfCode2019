using AdventOfCode2019.Seventeen;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DaySeventeenTests
    {
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DaySeventeen();
            var result = sut.PartA();

            Assert.Equal("4600", result);
        }

        // 10 is too low
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DaySeventeen();
            var result = sut.PartB();

            Assert.Equal("-1", result);
        }
    }
}