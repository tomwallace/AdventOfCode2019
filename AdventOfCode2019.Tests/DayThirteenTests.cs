using AdventOfCode2019.Thirteen;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayThirteenTests
    {
        [Fact]
        public void CountBlockTiles()
        {
            var input = new List<long>()
            {
                1, 2, 2, 6, 5, 4
            };
            var sut = new GameBoard(input);
            var result = sut.CountBlockTiles();

            Assert.Equal(1, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayThirteen();
            var result = sut.PartA();

            Assert.Equal("372", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayThirteen();
            var result = sut.PartB();

            Assert.Equal("1110", result);
        }
    }
}