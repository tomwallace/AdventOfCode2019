using System.Diagnostics;

namespace AdventOfCode2019.Eight
{
    public class ImageLayer
    {
        private int _rows;
        private int _cols;

        public Pixel[,] Pixels { get; set; }

        public int LayerId { get; set; }

        public ImageLayer(int rows, int cols, string layerInput, int layerId)
        {
            Pixels = new Pixel[rows, cols];
            LayerId = layerId;
            _rows = rows;
            _cols = cols;

            // Create the Pixels
            char[] splits = layerInput.ToCharArray();
            int rowPointer = 0;
            int colPointer = 0;
            for (int i = 0; i < splits.Length; i++)
            {
                char pixel = splits[i];
                Pixels[rowPointer, colPointer] = new Pixel(pixel);

                rowPointer++;
                if (rowPointer >= _rows)
                {
                    rowPointer = 0;
                    colPointer++;
                }
            }
        }

        // Creates a blank image layer
        public ImageLayer(int rows, int cols, int layerId)
        {
            Pixels = new Pixel[rows, cols];
            LayerId = layerId;
            _rows = rows;
            _cols = cols;
        }

        public int CountPixelColor(int color)
        {
            int instancesFound = 0;
            foreach (Pixel pixel in Pixels)
            {
                if (pixel.Color == color)
                    instancesFound++;
            }

            return instancesFound;
        }

        // 0 = black, 1 = white, 2 = transparent
        public void Print()
        {
            Debug.WriteLine("Printing Image Layer ---------------------------");
            Debug.WriteLine("");

            for (int colPointer = 0; colPointer < _cols; colPointer++)
            {
                string row = "";

                for (int rowPointer = 0; rowPointer < _rows; rowPointer++)
                {
                    int color = Pixels[rowPointer, colPointer].Color;
                    if (color == 0)
                        row = $"{row} ";
                    else if (color == 1)
                        row = $"{row}&";
                    else
                        row = $"{row} ";
                }

                Debug.WriteLine(row);
            }

            Debug.WriteLine("");
            Debug.WriteLine("---------------------------------------");
        }
    }
}