using AdventOfCode2019.TwentyTwo;
using System.Linq;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayTwentyTwoTests
    {
        [Fact]
        public void NewStack()
        {
            var input = Enumerable.Range(0, 5).ToArray();
            var sut = new NewStack("deal into new stack");
            var result = sut.Do(input);

            Assert.Equal(new[] { 4, 3, 2, 1, 0 }, result);
        }

        [Fact]
        public void CutCards()
        {
            var input = Enumerable.Range(0, 10).ToArray();
            var sut = new CutCards("cut 3");
            var result = sut.Do(input);

            Assert.Equal(new[] { 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 }, result);
        }

        [Fact]
        public void CutCards_Negative()
        {
            var input = Enumerable.Range(0, 10).ToArray();
            var sut = new CutCards("cut -4");
            var result = sut.Do(input);

            Assert.Equal(new[] { 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 }, result);
        }

        [Fact]
        public void IncrementCards()
        {
            var input = Enumerable.Range(0, 10).ToArray();
            var sut = new IncrementCards("deal with increment 3");
            var result = sut.Do(input);

            var output = new[] { 0, 7, 4, 1, 8, 5, 2, 9, 6, 3 };
            Assert.Equal(output, result);
        }

        [Theory]
        [InlineData(@"TwentyTwo\DayTwentyTwoTestInputB.txt", "0,3,6,9,2,5,8,1,4,7")]
        [InlineData(@"TwentyTwo\DayTwentyTwoTestInputC.txt", "3,0,7,4,1,8,5,2,9,6")]
        [InlineData(@"TwentyTwo\DayTwentyTwoTestInputD.txt", "6,3,0,7,4,1,8,5,2,9")]
        [InlineData(@"TwentyTwo\DayTwentyTwoTestInputA.txt", "9,2,5,8,1,4,7,0,3,6")]
        public void SpaceCardShuffler(string filePath, string expected)
        {
            int[] result = new SpaceCardShuffler(filePath, 10).Shuffle();
            int[] expectedConverted = expected.Split(',').Select(i => int.Parse(i)).ToArray();
            Assert.Equal(expectedConverted, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayTwentyTwo();
            var result = sut.PartA();

            Assert.Equal("6850", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayTwentyTwo();
            var result = sut.PartB();

            Assert.Equal("13224103523662", result);
        }
    }
}