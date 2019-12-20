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

        // 3930687 = too low
        // 3970694 = too low
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayNineteen();
            var result = sut.PartB();

            Assert.Equal("-1", result);
        }
    }
}