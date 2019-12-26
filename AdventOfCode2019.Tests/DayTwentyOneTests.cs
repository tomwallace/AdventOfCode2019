using AdventOfCode2019.TwentyOne;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayTwentyOneTests
    {
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwentyOne();
            var result = sut.PartA();

            Assert.Equal("19359996", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwentyOne();
            var result = sut.PartB();

            Assert.Equal("1143330711", result);
        }
    }
}