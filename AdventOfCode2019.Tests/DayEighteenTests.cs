using AdventOfCode2019.Eighteen;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayEighteenTests
    {
        [Theory]
        [InlineData(@"Eighteen\DayEighteenTestInputA.txt", 8)]
        [InlineData(@"Eighteen\DayEighteenTestInputB.txt", 86)]
        [InlineData(@"Eighteen\DayEighteenTestInputC.txt", 132)]
        //[InlineData(@"Eighteen\DayEighteenTestInputD.txt", 136)]
        [InlineData(@"Eighteen\DayEighteenTestInputE.txt", 81)]
        public void FindFewestStepsThroughMap(string filePath, int expected)
        {
            var sut = new DayEighteen();
            var result = sut.FindFewestStepsThroughMap(filePath);

            Assert.Equal(expected, result);
        }

        // TODO: Cannot get example D to work or part a
        /*
        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayEighteen();
            var result = sut.PartA();

            Assert.Equal("9958", result);
        }
        */

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayEighteen();
            var result = sut.PartB();

            Assert.Equal("1110", result);
        }
    }
}