using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Sixteen
{
    public class DaySixteen : IAdventProblemSet
    {
        public const string INPUT = "59717238168580010599012527510943149347930742822899638247083005855483867484356055489419913512721095561655265107745972739464268846374728393507509840854109803718802780543298141398644955506149914796775885246602123746866223528356493012136152974218720542297275145465188153752865061822191530129420866198952553101979463026278788735726652297857883278524565751999458902550203666358043355816162788135488915722989560163456057551268306318085020948544474108340969874943659788076333934419729831896081431886621996610143785624166789772013707177940150230042563041915624525900826097730790562543352690091653041839771125119162154625459654861922989186784414455453132011498";
        
        public string Description()
        {
            return "Flawed Frequency Transmission";
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
            string filePath = @"Ten\DayTenInput.txt";
            //int destroyed = RunAsteroidRoutine(filePath, 200);

            return ""; //destroyed.ToString();
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

        public string DecodeTransmission(string input, int numberOfTimesToApply, int returnCharacters)
        {
            int offset = int.Parse(input.Substring(0, 7));
            string modInput = input;

            // TODO: There must be a way to short circuit the process, as even the sample problem would take hours
            for (int i = 0; i < 10000; i++)
                modInput = $"{modInput}{input}";

            string result = ApplyFlawedFrequencyTransmission(modInput, numberOfTimesToApply, -1);

            return result.Substring(offset, returnCharacters);
        }
    }
}