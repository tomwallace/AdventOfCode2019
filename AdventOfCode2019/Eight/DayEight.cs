using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Eight
{
    public class DayEight : IAdventProblemSet
    {
        public string Description()
        {
            return "Space Image Format";
        }

        public int SortOrder()
        {
            return 8;
        }

        public string PartA()
        {
            string filePath = @"Eight\DayEightInput.txt";
            int calculation = CalculationOnLayerWithFewestZeros(filePath, 25, 6);

            return calculation.ToString();
        }

        public string PartB()
        {
            string filePath = @"Eight\DayEightInput.txt";
            Image image = CreateImage(filePath, 25, 6);

            ImageLayer decodedLayer = image.Decode();
            decodedLayer.Print();

            // Solution is visible, so we just want to return something to match the interface
            return "1";
        }

        public int CalculationOnLayerWithFewestZeros(string filePath, int rows, int cols)
        {
            Image image = CreateImage(filePath, rows, cols);

            List<ImageLayer> sorted = image.Layers.OrderBy(l => l.CountPixelColor(0)).ToList();
            ImageLayer fewestZeros = sorted[0];

            int calculation = fewestZeros.CountPixelColor(1) * fewestZeros.CountPixelColor(2);
            return calculation;
        }

        private Image CreateImage(string filePath, int rows, int cols)
        {
            Image image = new Image(rows, cols);

            // The input is only one line
            List<string> fileLines = FileUtility.ParseFileToList(filePath, line => line);
            string input = fileLines.First();

            for (int i = 0; i < input.Length; i += (rows * cols))
            {
                string layerInput = input.Substring(i, rows * cols);
                image.AddLayer(layerInput);
            }

            return image;
        }
    }
}