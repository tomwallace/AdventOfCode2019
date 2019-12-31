using AdventOfCode2019.Utility;
using System;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2019.TwentyTwo
{
    public static class DayTwentyTwoOther
    {
        // Could not figure out the math on my own, so used the solution from here for Part B
        // https://github.com/kbmacneal/adv_of_code_2019/blob/3bdc583ea5620296e187a076038ddde17e526abd/days/22.cs
        public static BigInteger Run(string filePath)
        {
            var input = FileUtility.ParseFileToList(filePath, l => l).ToArray();

            return Part2(input);
        }

        private static BigInteger Part2(string[] inputs)
        {
            BigInteger size = 119315717514047;
            BigInteger iter = 101741582076661;
            BigInteger position = 2020;
            BigInteger offset_diff = 0;
            BigInteger increment_mul = 1;

            foreach (var line in inputs)
            {
                RunP2(ref increment_mul, ref offset_diff, size, line);
            }

            var dto = getseq(iter, increment_mul, offset_diff, size);

            var card = get(dto, 2020, size);

            return card;
        }

        private static void RunP2(ref BigInteger inc_mul, ref BigInteger offset_diff, BigInteger size, string line)
        {
            if (line.StartsWith("cut"))
            {
                offset_diff += Int32.Parse(line.Split(' ').Last()) * inc_mul;
            }
            else if (line == "deal into new stack")
            {
                inc_mul *= -1;
                offset_diff += inc_mul;
            }
            else
            {
                var num = Int32.Parse(line.Split(' ').Last());

                inc_mul *= num.TBI().inv(size);
            }

            inc_mul = inc_mul.mod(size);
            offset_diff = offset_diff.mod(size);
        }

        private static BigInteger mod(this BigInteger x, BigInteger m)
        {
            return (x % m + m) % m;
        }

        private static BigInteger inv(this BigInteger num, BigInteger size)
        {
            return num.mpow(size - 2, size);
        }

        private static BigInteger get(Dto dto, BigInteger i, BigInteger size)
        {
            return (dto.Offset + i * dto.Increment) % size;
        }

        private static Dto getseq(this BigInteger iterations, BigInteger inc_mul, BigInteger offset_diff, BigInteger size)
        {
            var increment = inc_mul.mpow(iterations, size);

            var offset = offset_diff * (1 - increment) * ((1 - inc_mul) % size).inv(size);

            offset %= size;

            return new Dto(increment, offset);
        }

        private static BigInteger TBI(this int num)
        {
            return new BigInteger(num);
        }

        private static BigInteger mpow(this BigInteger bigInteger, BigInteger pow, BigInteger mod)
        {
            return BigInteger.ModPow(bigInteger, pow, mod);
        }
    }

    public class Dto
    {
        public BigInteger Increment { get; set; }

        public BigInteger Offset { get; set; }

        public Dto(BigInteger increment, BigInteger offset)
        {
            Increment = increment;
            Offset = offset;
        }
    }
}