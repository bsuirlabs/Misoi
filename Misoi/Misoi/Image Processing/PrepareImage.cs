using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misoi.Image_Processing
{
    class PrepareImage : IPrepareImage
    {
        public PrepareImage()
        {

        }

        public Color[,] GetPixels(string filePath)
        {
            Bitmap bitmap = new Bitmap(filePath);
            Color[,] pixels = new Color[bitmap.Height, bitmap.Width];
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    pixels[y, x] = bitmap.GetPixel(x, y);
                }
            }
            return pixels;
        }
    }
}
