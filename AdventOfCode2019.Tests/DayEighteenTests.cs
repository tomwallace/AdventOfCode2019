using AdventOfCode2019.Eighteen;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayEighteenTests
    {
        [Theory]
        [InlineData(@"Eighteen\DayEighteenTestInputA.txt", 8)]
        [InlineData(@"Eighteen\DayEighteenTestInputB.txt", 86)]
        [InlineData(@"Eighteen\DayEighteenTestInputC.txt", 132)]
        [InlineData(@"Eighteen\DayEighteenTestInputD.txt", 136)]
        [InlineData(@"Eighteen\DayEighteenTestInputE.txt", 81)]
        public async Task FindFewestStepsThroughMap(string filePath, int expected)
        {
            var sut = new DayEighteenNew();
            var result = await sut.FindFewestStepsThroughMap(filePath);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(@"Eighteen\DayEighteenTestInputF.txt", 8)]
        [InlineData(@"Eighteen\DayEighteenTestInputG.txt", 24)]
        [InlineData(@"Eighteen\DayEighteenTestInputH.txt", 72)]
        [InlineData(@"Eighteen\DayEighteenTestInputI.txt", 32)]
        public async Task FindStepsWithMultipleRobots(string filePath, int expected)
        {
            var sut = new DayEighteenNew();
            var result = await sut.FindStepsWithMultipleRobots(filePath);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayEighteenNew();
            var result = sut.PartA();

            Assert.Equal("3862", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayEighteenNew();
            var result = sut.PartB();

            Assert.Equal("1626", result);
        }
    }
}