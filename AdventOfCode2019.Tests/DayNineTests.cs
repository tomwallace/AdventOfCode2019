using AdventOfCode2019.Nine;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayNineTests
    {
        [Theory]
        [InlineData("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", 0, 99)]
        [InlineData("1102,34915192,34915192,7,4,7,99,0", 2, 1219070632396864)]
        [InlineData("104,1125899906842624,99", 0, 1125899906842624)]
        public void CalculateBoostKeycode(string input, long startingInput, long expected)
        {
            var sut = new DayNine();
            var result = sut.CalculateBoostKeycode(input, startingInput);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayNine();
            var result = sut.PartA();

            Assert.Equal("3518157894", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayNine();
            var result = sut.PartB();

            Assert.Equal("80379", result);
        }
    }
}