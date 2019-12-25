using AdventOfCode2019.TwentyThree;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayTwentyThreeTests
    {
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwentyThree();
            var result = sut.PartA();

            Assert.Equal("24954", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwentyThree();
            var result = sut.PartB();

            Assert.Equal("17091", result);
        }
    }
}