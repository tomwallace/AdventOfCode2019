using AdventOfCode2019.IntCodeComputer;
using AdventOfCode2019.Two;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayTwoTests
    {
        [Theory]
        [InlineData("1,0,0,0,99", 0, 0, 2)]
        [InlineData("2,3,0,3,99", 3, 0, 2)]
        [InlineData("2,4,4,5,99,0", 4, 4, 2)]
        [InlineData("1,1,1,4,99,5,6,0,99", 1, 1, 30)]
        public void ProcessIntcodeProgram(string input, int noun, int verb, int expected)
        {
            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(input, noun, verb);
            computer.ProcessInstructions();

            Assert.Equal(expected, computer.Result());
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwo();
            var result = sut.PartA();

            Assert.Equal("3085697", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwo();
            var result = sut.PartB();

            Assert.Equal("9425", result);
        }
    }
}