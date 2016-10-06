using System;
using System.Drawing;

namespace Misoi.Image_Processing
{
    class PrepareImage : IPrepareImage
    {
        public PrepareImage()
        {

        }

        private Color[,] GetPixels(Bitmap bitmap)
        {
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

        public Bitmap FilterImage(Bitmap bitmap)
        {
            var filteredImage = MedianFilter(bitmap);
            filteredImage = MonochromeFilter(filteredImage, 195);
            //filteredImage = BradleyFilter(filteredImage);
            return filteredImage;
        }

        private Bitmap MonochromeFilter(Bitmap bitmap, int level)
        {
            var outImage = new Bitmap(bitmap);
            for (int j = 0; j < outImage.Height; j++)
            {
                for (int i = 0; i < outImage.Width; i++)
                {
                    var color = outImage.GetPixel(i, j);
                    double sr = (color.R + color.G + color.B) / 3;
                    outImage.SetPixel(i, j, (sr < level ? Color.Black : Color.White));
                }
            }
            return outImage;
        }

        private Bitmap BradleyFilter(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            var sum = 0;
            int count;
            var s = width / 8;
            double t = 99;
            int[,] intImage = new int[width, height];
            var outImage = new Bitmap(bitmap);
            for (var i = 0; i < width; i++)
            {
                sum = 0;
                for (var j = 0; j < height; j++)
                {
                    sum = sum + getAverageColor(bitmap.GetPixel(i, j));
                    if (i == 0)
                    {
                        intImage[i, j] = sum;
                    }
                    else
                    {
                        intImage[i, j] = intImage[i - 1, j] + sum;
                    }
                }
            }

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    //if (OutOfBound(width, height, i, j, s))
                    //{
                    //    continue;
                    //}

                    var x1 = (i - s / 2) >= 0 ? i - s / 2 : 0;
                    var x2 = (i + s / 2) < width ? i + s / 2 : width - 1;
                    var y1 = (j - s / 2) >= 0 ? j - s / 2 : 0;
                    var y2 = (j + s / 2) < height ? j + s / 2 : height - 1;

                    //var x1 = i - s/2;
                    //var x2 = i + s/2;
                    //var y1 = j - s/2;
                    //var y2 = j + s/2;

                    count = (x2 - x1) * (y2 - y1);
                    if (OutOfArrayBounds(x1, y1))
                    {
                        continue;
                    }
                    sum = intImage[x2, y2] - intImage[x2, y1 - 1] - intImage[x1 - 1, y2] + intImage[x1 - 1, y1 - 1];
                    if (getAverageColor(bitmap.GetPixel(i, j)) * count <= (sum * (100 - t) / 100))
                    {
                        outImage.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        outImage.SetPixel(i, j, Color.Black);
                    }
                }
            }
            return outImage;
        }

        private bool OutOfArrayBounds(int x1, int y1)
        {
            return (x1 - 1 < 0) || (y1 - 1 < 0);
        }

        private bool OutOfBound(int width, int height, int xPos, int yPos, int s)
        {
            return (xPos - s / 2 < 0) || (xPos + s / 2 > width) || (yPos - s / 2 < 0) || (yPos + s / 2 > height);
        }

        private int getAverageColor(Color pixel)
        {
            return (pixel.R + pixel.B + pixel.G) / 3;
        }

        private Bitmap MedianFilter(Bitmap bitmap)
        {
            var arrR = new int[8];
            var arrG = new int[8];
            var arrB = new int[8];
            var outImage = new Bitmap(bitmap);
            for (int i = 1; i < bitmap.Width - 1; i++)
            {
                for (int j = 1; j < bitmap.Height; j++)
                {
                    for (int i1 = 0; i1 < 2; i1++)
                    {
                        for (int j1 = 0; j1 < 2; j1++)
                        {
                            var p = bitmap.GetPixel(i + i1 - 1, j + j1 - 1);

                            arrR[i1 * 3 + j1] = ((p.R + p.G + p.B) / 3) ;
                            arrG[i1 * 3 + j1] = ((p.R + p.G + p.B) / 3) ;
                            arrB[i1 * 3 + j1] = ((p.R + p.G + p.B) / 3) ;
                        }
                    }
                    Array.Sort(arrR);
                    Array.Sort(arrG);
                    Array.Sort(arrB);

                    outImage.SetPixel(i, j, Color.FromArgb(arrR[4], arrG[4], arrB[4]));
                }
            }
            return outImage;
        }

    }
}
