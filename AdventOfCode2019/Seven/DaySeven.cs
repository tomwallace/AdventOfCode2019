using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Seven
{
    public class DaySeven : IAdventProblemSet
    {
        public string Description()
        {
            return "Amplification Circuit [IntCodeComputer]";
        }

        public int SortOrder()
        {
            return 7;
        }

        public string PartA()
        {
            string filePath = @"Seven\DaySevenInput.txt";
            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string memoryInput = fileLines.First();

            long result = CalculateMaxSignalToThrusters(memoryInput);
            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"Seven\DaySevenInput.txt";
            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string memoryInput = fileLines.First();

            long result = FeedbackLoopCalculateMaxSignal(memoryInput);
            return result.ToString();
        }

        public long CalculateMaxSignalToThrusters(string memoryInput)
        {
            HashSet<long> thrusterSignals = new HashSet<long>();
            var possibleSettings = GetPermutations(Enumerable.Range(0, 5), 5);

            foreach (var settings in possibleSettings)
            {
                long amplifierSignal = 0;

                foreach (int phaseSetting in settings)
                {
                    long[] inputs = new[] { phaseSetting, amplifierSignal };
                    IntCodeComputer.IntCodeComputer amplifier = new IntCodeComputer.IntCodeComputer(memoryInput, inputs);
                    amplifier.ProcessInstructions();
                    amplifierSignal = amplifier.GetDiagnosticCode();
                }

                thrusterSignals.Add(amplifierSignal);
            }

            List<long> sortedSignals = thrusterSignals.ToList().OrderByDescending(s => s).ToList();
            return sortedSignals.FirstOrDefault();
        }

        public long FeedbackLoopCalculateMaxSignal(string memoryInput)
        {
            HashSet<long> thrusterSignals = new HashSet<long>();
            var possibleSettings = GetPermutations(Enumerable.Range(5, 5), 5);

            foreach (var settings in possibleSettings)
            {
                // Cannot loop over collection of IntCodeComputers, as for this part, they need to remain as they were in previous run
                IntCodeComputer.IntCodeComputer amplifierA = new IntCodeComputer.IntCodeComputer(memoryInput, new long[] { 0 }, true);
                IntCodeComputer.IntCodeComputer amplifierB = new IntCodeComputer.IntCodeComputer(memoryInput, new long[] { 0 }, true);
                IntCodeComputer.IntCodeComputer amplifierC = new IntCodeComputer.IntCodeComputer(memoryInput, new long[] { 0 }, true);
                IntCodeComputer.IntCodeComputer amplifierD = new IntCodeComputer.IntCodeComputer(memoryInput, new long[] { 0 }, true);
                IntCodeComputer.IntCodeComputer amplifierE = new IntCodeComputer.IntCodeComputer(memoryInput, new long[] { 0 }, true);

                bool initSetup = true;
                int exitCode = 0;
                long amplifierSignal = 0;
                int[] phaseSettings = settings.ToArray();
                do
                {
                    long[] inputsA = initSetup ? new[] { phaseSettings[0], amplifierSignal } : new[] { amplifierSignal };
                    amplifierA.SetInput(inputsA);
                    exitCode += amplifierA.ProcessInstructions();
                    amplifierSignal = amplifierA.GetDiagnosticCode();

                    long[] inputsB = initSetup ? new[] { phaseSettings[1], amplifierSignal } : new[] { amplifierSignal };
                    amplifierB.SetInput(inputsB);
                    exitCode += amplifierB.ProcessInstructions();
                    amplifierSignal = amplifierB.GetDiagnosticCode();

                    long[] inputsC = initSetup ? new[] { phaseSettings[2], amplifierSignal } : new[] { amplifierSignal };
                    amplifierC.SetInput(inputsC);
                    exitCode += amplifierC.ProcessInstructions();
                    amplifierSignal = amplifierC.GetDiagnosticCode();

                    long[] inputsD = initSetup ? new[] { phaseSettings[3], amplifierSignal } : new[] { amplifierSignal };
                    amplifierD.SetInput(inputsD);
                    exitCode += amplifierD.ProcessInstructions();
                    amplifierSignal = amplifierD.GetDiagnosticCode();

                    long[] inputsE = initSetup ? new[] { phaseSettings[4], amplifierSignal } : new[] { amplifierSignal };
                    amplifierE.SetInput(inputsE);
                    exitCode += amplifierE.ProcessInstructions();
                    amplifierSignal = amplifierE.GetDiagnosticCode();

                    if (initSetup)
                        initSetup = false;
                } while (exitCode == 0);

                thrusterSignals.Add(amplifierSignal);
            }

            List<long> sortedSignals = thrusterSignals.ToList().OrderByDescending(s => s).ToList();
            return sortedSignals.FirstOrDefault();
        }

        //Taken wholesale from https://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
        private IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}