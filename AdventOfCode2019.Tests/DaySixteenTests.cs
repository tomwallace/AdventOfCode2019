using AdventOfCode2019.Sixteen;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DaySixteenTests
    {
        [Theory]
        [InlineData("12345678", 4, "01029498")]
        [InlineData("80871224585914546619083218645595", 100, "24176176")]
        [InlineData("19617804207202209144916044189917", 100, "73745418")]
        [InlineData("69317163492948606335995924319873", 100, "52432133")]
        public void ApplyFlawedFrequencyTransmission(string input, int numberOfTimesToApply, string expected)
        {
            var sut = new DaySixteen();
            var result = sut.ApplyFlawedFrequencyTransmission(input, numberOfTimesToApply, 8);

            Assert.Equal(expected, result);
        }

        // TODO: Figure out a way to short cut these tests and re-enable
        [Theory]
        [InlineData("03036732577212944063491565474664", 100, "84462026")]
        //[InlineData("02935109699940807407585447034323", 100, "78725270")]
        //[InlineData("03081770884921959731165446850517", 100, "53553731")]
        public void DecodeTransmission(string input, int numberOfTimesToApply, string expected)
        {
            var sut = new DaySixteen();
            var result = sut.DecodeTransmission(input, numberOfTimesToApply, 8);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DaySixteen();
            var result = sut.PartA();

            Assert.Equal("58672132", result);
        }

        // 10 is too low
        [Fact]
        public void PartB_Actual()
        {
            var sut = new DaySixteen();
            var result = sut.PartB();

            Assert.Equal("310", result);
        }
    }
}