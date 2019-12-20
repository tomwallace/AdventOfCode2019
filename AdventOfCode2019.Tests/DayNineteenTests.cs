using AdventOfCode2019.Nineteen;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayNineteenTests
    {
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayNineteen();
            var result = sut.PartA();

            Assert.Equal("176", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayNineteen();
            var result = sut.PartB();

            Assert.Equal("6751081", result);
        }
    }
}