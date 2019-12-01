using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.LastYearOne
{
    public class LastYearDayOne : IAdventProblemSet
    {
        public string Description()
        {
            return "Chronal Calibration";
        }

        public int SortOrder()
        {
            return 1;
        }

        public string PartA()
        {
            string filePath = @"LastYearOne\DayOneInput.txt";
            int largest = ChangeFrequency(filePath, 0);

            return largest.ToString();
        }

        public string PartB()
        {
            string filePath = @"LastYearOne\DayOneInput.txt";
            int duplicate = FindFirstDuplicateFrequency(filePath, 0);

            return duplicate.ToString();
        }

        public int ChangeFrequency(string filePath, int startingFrequency)
        {
            List<int> alterations = ParseForSplitInts(filePath);
            int? duplicate = null;

            HashSet<int> accumulatedFrequencies = ProcessFrequencies(alterations, new HashSet<int>(), startingFrequency, out duplicate);

            return accumulatedFrequencies.Last();
        }

        public int FindFirstDuplicateFrequency(string filePath, int startingFrequency)
        {
            List<int> alterations = ParseForSplitInts(filePath);
            HashSet<int> accumulatedFrequencies = new HashSet<int>() { startingFrequency };
            int currentFrquency = startingFrequency;
            int? duplicate = null;
            do
            {
                accumulatedFrequencies = ProcessFrequencies(alterations, accumulatedFrequencies, currentFrquency, out duplicate);
                currentFrquency = accumulatedFrequencies.Last();
            } while (duplicate == null);

            return duplicate.Value;
        }

        // I am not fond of the out variable to detect the duplictes, but it is easier than parsing the accumulated frequencies for the first duplicate on every run
        private HashSet<int> ProcessFrequencies(List<int> alterations, HashSet<int> accumulatedFrequencies, int startingFrequency, out int? duplicate)
        {
            duplicate = null;
            int currentFrequency = startingFrequency;

            foreach (int alteration in alterations)
            {
                currentFrequency += alteration;

                if (accumulatedFrequencies.Contains(currentFrequency) && duplicate == null)
                    duplicate = currentFrequency;

                accumulatedFrequencies.Add(currentFrequency);
            }

            return accumulatedFrequencies;
        }

        private List<int> ParseForSplitInts(string filePath)
        {
            List<int> splitInts = new List<int>();
            string line;
            StreamReader file = new StreamReader(filePath);

            // Iterate over each line in the input
            while ((line = file.ReadLine()) != null)
            {
                splitInts.Add(int.Parse(line.Trim()));
            }
            file.Close();
            return splitInts;
        }
    }
}