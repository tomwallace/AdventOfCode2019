using Xunit;

namespace AdventOfCode2019.Tests
{
    public class IntCodeComputerTests
    {
        [Theory]
        [InlineData("1,0,0,0,99", 0, 0, 2)]
        [InlineData("2,3,0,3,99", 3, 0, 2)]
        [InlineData("2,4,4,5,99,0", 4, 4, 2)]
        [InlineData("1,1,1,4,99,5,6,0,99", 1, 1, 30)]
        public void DayTwo_ProcessIntCodeComputer(string input, int noun, int verb, int expected)
        {
            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(input, noun, verb);
            computer.ProcessInstructions();

            Assert.Equal(expected, computer.GetMemorySlotOne());
        }

        [Fact]
        public void DayFive_BasicTestScenario()
        {
            var sut = new IntCodeComputer.IntCodeComputer("1002,4,3,4,33", 1);
            sut.ProcessInstructions();
            var result = sut.GetDiagnosticCode();

            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 7, 0)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 9, 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 7, 1)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 9, 0)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 7, 0)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 9, 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 7, 1)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 8, 0)]
        public void DayFive_CompareTests(string memoryInput, int input, int expected)
        {
            var sut = new IntCodeComputer.IntCodeComputer(memoryInput, input);
            sut.ProcessInstructions();
            var result = sut.GetDiagnosticCode();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, 0)]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 7, 1)]
        [InlineData("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 0, 0)]
        [InlineData("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 7, 1)]
        public void DayFive_JumpTests(string memoryInput, int input, int expected)
        {
            var sut = new IntCodeComputer.IntCodeComputer(memoryInput, input);
            sut.ProcessInstructions();
            var result = sut.GetDiagnosticCode();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void DayFive_LargeExampleTest()
        {
            var sut = new IntCodeComputer.IntCodeComputer("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 8);
            sut.ProcessInstructions();
            var result = sut.GetDiagnosticCode();

            Assert.Equal(1000, result);
        }
    }
}