using AdventOfCode2019.Eleven;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayElevenTests
    {
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayEleven();
            var result = sut.PartA();

            Assert.Equal("2319", result);
        }

        // Answer is UERPRFGJ
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayEleven();
            var result = sut.PartB();

            Assert.Equal("250", result);
        }
    }
}