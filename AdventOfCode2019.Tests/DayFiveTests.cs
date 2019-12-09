using AdventOfCode2019.Five;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayFiveTests
    {
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayFive();
            var result = sut.PartA();

            Assert.Equal("5074395", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayFive();
            var result = sut.PartB();

            Assert.Equal("8346937", result);
        }
    }
}