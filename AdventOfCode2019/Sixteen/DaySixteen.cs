using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Sixteen
{
    public class DaySixteen : IAdventProblemSet
    {
        public const string INPUT = "59717238168580010599012527510943149347930742822899638247083005855483867484356055489419913512721095561655265107745972739464268846374728393507509840854109803718802780543298141398644955506149914796775885246602123746866223528356493012136152974218720542297275145465188153752865061822191530129420866198952553101979463026278788735726652297857883278524565751999458902550203666358043355816162788135488915722989560163456057551268306318085020948544474108340969874943659788076333934419729831896081431886621996610143785624166789772013707177940150230042563041915624525900826097730790562543352690091653041839771125119162154625459654861922989186784414455453132011498";

        private static byte[] _cache;

        public string Description()
        {
            return "Flawed Frequency Transmission [HARD]";
        }

        public int SortOrder()
        {
            return 16;
        }

        public string PartA()
        {
            string result = ApplyFlawedFrequencyTransmission(INPUT, 100, 8);

            return result;
        }

        public string PartB()
        {
            string result = DecodeTransmission(INPUT, 10000);

            return result;
        }

        public string ApplyFlawedFrequencyTransmission(string input, int numberOfTimesToApply, int returnCharacters)
        {
            string runningInput = input;
            Dictionary<string, long> attempts = new Dictionary<string, long>();
            long stepFirstDuplicated = -1;

            for (long iteration = 0; iteration < numberOfTimesToApply; iteration++)
            {
                if (attempts.Keys.Any(k => k.Contains(runningInput)))
                    stepFirstDuplicated = attempts[runningInput];
                else
                    attempts.Add(runningInput, iteration);

                int[] signal = runningInput.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
                int[] baseRepeatingPattern = new[] { 0, 1, 0, -1 };

                int[] updatedSignal = new int[signal.Length];

                for (int outerSignal = 0; outerSignal < signal.Length; outerSignal++)
                {
                    int signalPart = 0;
                    int repeatingPointer = 0;
                    int repeatLoop = 1;
                    for (int i = 0; i < signal.Length; i++)
                    {
                        // TODO: Fix the repeating pattern, as accurate for first run, but does not iterate then
                        if (repeatLoop > outerSignal)
                        {
                            repeatingPointer++;
                            repeatLoop = 0;
                        }

                        repeatLoop++;

                        if (repeatingPointer >= baseRepeatingPattern.Length)
                            repeatingPointer = 0;

                        signalPart += signal[i] * baseRepeatingPattern[repeatingPointer];
                    }

                    updatedSignal[outerSignal] = Math.Abs(signalPart % 10);
                }

                runningInput = string.Join("", updatedSignal);
            }

            if (returnCharacters < 0)
                return runningInput.Substring(0);

            return runningInput.Substring(0, returnCharacters);
        }

        // Could not figure out the short cut, so used a solution from here - https://github.com/XorZy/Aoc_2019_Day_16/blob/master/Program.cs
        public string DecodeTransmission(string inputComingIn, int repeats)
        {
            var input = inputComingIn.Select(x => (byte)(x - '0')).ToArray();

            //int repeats = 10_000;

            var adjustedArray = new byte[input.Length * repeats];

            for (int i = 0; i < repeats; i++)
            {
                Buffer.BlockCopy(input, 0, adjustedArray, input.Length * i, input.Length);
            }

            var offset = int.Parse(string.Join("", input.Take(7)));

            _cache = new byte[adjustedArray.Length];
            for (int i = 0; i < 100; i++)
            {
                Round(ref adjustedArray, offset);
            }

            return string.Join("", adjustedArray.Skip(offset).Take(8));
        }

        private void Round(ref byte[] input, int from = 0)
        {
            var longsum = 0;

            for (int k = from; k < input.Length; k++)
            {
                longsum += input[k];
            }

            for (int i = from; i < input.Length; i++)
            {
                _cache[i] = (byte)(longsum % 10);
                longsum -= input[i];
            }

            var tmp = input;

            input = _cache;
            _cache = tmp;
        }
    }
}