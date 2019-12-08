using AdventOfCode2019.Eight;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayEightTests
    {
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayEight();
            var result = sut.PartA();

            Assert.Equal("2016", result);
        }

        // Printed image was - HCZCU
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayEight();
            var result = sut.PartB();

            Assert.Equal("1", result);
        }
    }
}