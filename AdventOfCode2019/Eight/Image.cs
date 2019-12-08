using System.Collections.Generic;

namespace AdventOfCode2019.Eight
{
    public class Image
    {
        private int _rows;
        private int _cols;
        private int _highestId;

        public List<ImageLayer> Layers { get; set; }

        public Image(int rows, int cols)
        {
            Layers = new List<ImageLayer>();
            _rows = rows;
            _cols = cols;
            _highestId = 0;
        }

        public void AddLayer(string layerInput)
        {
            Layers.Add(new ImageLayer(_rows, _cols, layerInput, _highestId));
            _highestId++;
        }

        // Decodes the image by looking at each pixel in the layers, stacked on top of each other
        // 0 = black, 1 = white, 2 = transparent
        // You ignore the transparent ones and find the first colored one, going in the layers top to back
        public ImageLayer Decode()
        {
            ImageLayer decodedLayer = new ImageLayer(_rows, _cols, 99);
            for (int rowPointer = 0; rowPointer < _rows; rowPointer++)
            {
                for (int colPointer = 0; colPointer < _cols; colPointer++)
                {
                    for (int layerPointer = 0; layerPointer < Layers.Count; layerPointer++)
                    {
                        Pixel currentPixel = Layers[layerPointer].Pixels[rowPointer, colPointer];
                        if (currentPixel.Color != 2)
                        {
                            decodedLayer.Pixels[rowPointer, colPointer] = new Pixel(currentPixel.Color);
                            break;
                        }
                    }
                }
            }

            return decodedLayer;
        }
    }
}