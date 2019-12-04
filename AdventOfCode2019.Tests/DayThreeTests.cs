using AdventOfCode2019.Three;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class DayThreeTests
    {
        [Fact]
        public void CalculateClosestWireIntersection_FirstSample()
        {
            var wires = new List<string>()
            {
                "R8,U5,L5,D3",
                "U7,R6,D4,L4"
            };
            var sut = new DayThree();

            var result = sut.CalculateClosestWireIntersection(wires);

            Assert.Equal(6, result);
        }

        [Fact]
        public void CalculateClosestWireIntersection_SecondSample()
        {
            var wires = new List<string>()
            {
                "R75,D30,R83,U83,L12,D49,R71,U7,L72",
                "U62,R66,U55,R34,D71,R55,D58,R83"
            };
            var sut = new DayThree();

            var result = sut.CalculateClosestWireIntersection(wires);

            Assert.Equal(159, result);
        }

        [Fact]
        public void CalculateClosestWireIntersection_ThirdSample()
        {
            var wires = new List<string>()
            {
                "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
                "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"
            };
            var sut = new DayThree();

            var result = sut.CalculateClosestWireIntersection(wires);

            Assert.Equal(135, result);
        }

        [Fact]
        public void CalculateClosestWireSteps_FirstSample()
        {
            var wires = new List<string>()
            {
                "R8,U5,L5,D3",
                "U7,R6,D4,L4"
            };
            var sut = new DayThree();

            var result = sut.CalculateClosestWireSteps(wires);

            Assert.Equal(30, result);
        }

        [Fact]
        public void CalculateClosestWireSteps_SecondSample()
        {
            var wires = new List<string>()
            {
                "R75,D30,R83,U83,L12,D49,R71,U7,L72",
                "U62,R66,U55,R34,D71,R55,D58,R83"
            };
            var sut = new DayThree();

            var result = sut.CalculateClosestWireSteps(wires);

            Assert.Equal(610, result);
        }

        [Fact]
        public void CalculateClosestWireSteps_ThirdSample()
        {
            var wires = new List<string>()
            {
                "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
                "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"
            };
            var sut = new DayThree();

            var result = sut.CalculateClosestWireSteps(wires);

            Assert.Equal(410, result);
        }

        [Fact]
        public void PartA_Actual()
        {
            var sut = new DayThree();
            var result = sut.PartA();

            Assert.Equal("1626", result);
        }

        [Fact]
        public void PartB_Actual()
        {
            var sut = new DayThree();
            var result = sut.PartB();

            Assert.Equal("27330", result);
        }
    }
}