using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    public class Helpers:IHelper
    {
        public Neuron[] GetNeurons(Bitmap letter)
        {
            Neuron[] neuroWeb = new Neuron[52];
            for (int i = 0; i < 52; i++)
            {
                neuroWeb[i] = new Neuron
                {
                    Output = 0,
                    Name = i < 26 ? (char)((int)'a' + i) : (char)((int)'A' + (i - 26)),
                    Input = ReadInputArray(letter),
                    Memory = i < 26 ? ReadArray("patterns", (char)((int)'a' + i)) : ReadArray("patterns", (char)((int)'A' + (i - 26))),
                    Weight = 0
                };
            }
            return neuroWeb;
        }

        private Color[,] ReadInputArray(Bitmap letter)
        {
            var pixels = new Color[60, 120];
            var image = new Bitmap(letter);
            for (int x = 0; x < 60; x++)
            {
                for (int y = 0; y < 120; y++)
                {
                    pixels[x, y] = image.GetPixel(x, y);
                }
            }
            image.Dispose();

            return pixels;
        }

        private Color[,] ReadArray(string path, char letterName)
        {
            var pixels = new Color[60, 120];
            if (CheckWeHaveThatLetter(letterName))
            {
                var s = "D:\\temp\\" + path + "\\" + letterName + ".bmp";
                var image = new Bitmap(s);
                for (int x = 0; x < 60; x++)
                {
                    for (int y = 0; y < 120; y++)
                    {
                        pixels[x, y] = image.GetPixel(x, y);
                    }
                }
                image.Dispose();

            }
            return pixels;

        }

        private bool CheckWeHaveThatLetter(char letterName)
        {
            return letterName == 'a' || letterName == 'b' ||
                   letterName == 'c' || letterName == 'o' ||
                   letterName == 'p' || letterName == 't' ||
                   letterName == 'x';
        }

        public class Neuron
        {
            public char Name { get; set; }

            public Color[,] Input { get; set; }

            public int Output { get; set; }

            public Color[,] Memory { get; set; }

            public int Weight { get; set; }
        }
    }
}
