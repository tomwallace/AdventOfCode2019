using AdventOfCode2019.LastYearOne;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayOneTests
    {
        [Theory]
        [InlineData(@"LastYearOne\DayOneTestInputA.txt", 3)]
        [InlineData(@"LastYearOne\DayOneTestInputB.txt", 0)]
        [InlineData(@"LastYearOne\DayOneTestInputC.txt", -6)]
        public void ChangeFrequency(string input, int expected)
        {
            var sut = new LastYearDayOne();
            var result = sut.ChangeFrequency(input, 0);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(@"LastYearOne\DayOneTestInputD.txt", 0)]
        [InlineData(@"LastYearOne\DayOneTestInputE.txt", 10)]
        [InlineData(@"LastYearOne\DayOneTestInputF.txt", 5)]
        [InlineData(@"LastYearOne\DayOneTestInputG.txt", 14)]
        public void FindFirstDuplicateFrequency(string input, int expected)
        {
            var sut = new LastYearDayOne();
            var result = sut.FindFirstDuplicateFrequency(input, 0);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new LastYearDayOne();
            var result = sut.PartA();

            Assert.Equal("497", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new LastYearDayOne();
            var result = sut.PartB();

            Assert.Equal("558", result);
        }
    }
}