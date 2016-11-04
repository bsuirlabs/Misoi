using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filtering
{
    public class ApplyFilters: IFiltering
    {
        private Color[,] _allPixels { get; set; }

        public Bitmap FiterPicture(Bitmap pictureToProceed, bool applyMedian, bool applyMonochrome, int? level, int windowSize)
        {
            if (applyMedian)
            {
                pictureToProceed = ApplyMedianFilter(pictureToProceed, windowSize);
            }
            if (applyMonochrome && level.HasValue)
            {
                pictureToProceed = ApplyMonochrom(pictureToProceed, level.Value);
            }
            return pictureToProceed;
        }

        private Bitmap ApplyMonochrom(Bitmap inputPicture, int level)
        {
            //int level = getOtsuThreshold(inputPicture);
            var outputImage = new Bitmap(inputPicture.Width, inputPicture.Height);
            Color color = new Color();
            for (int i = 0; i < inputPicture.Width; i++)
            {
                for (int j = 0; j < inputPicture.Height; j++)
                {
                    color = inputPicture.GetPixel(i, j);
                    int medianValueColors = (color.R + color.G + color.B) / 3;
                    outputImage.SetPixel(i, j, (medianValueColors <= level ? Color.Black : Color.White));
                }
            }

            return outputImage;
        }

        private Bitmap ApplyMedianFilter(Bitmap inputImage, int size)
        {
            _allPixels = GetPixels(inputImage);
            var width = inputImage.Width;
            var height = inputImage.Height;
            var outputImage = new Bitmap(width - size / 2 + 1, height - size / 2 + 1);
            Color medianPixel;
            for (int i = size / 2; i < width - (size / 2 + 1); i++)
            {
                for (int j = size / 2; j < height - (size / 2 + 1); j++)
                {
                    medianPixel = SearchMedianPixel(i, j, size);
                    outputImage.SetPixel(i - size / 2, j - size / 2, medianPixel);
                }
            }
            return outputImage;
        }

        private Color[,] GetPixels(Bitmap image)
        {
            Color[,] allPixels = new Color[image.Width - 1, image.Height - 1];
            for (int i = 0; i < (image.Width - 1); i++)
            {
                for (int j = 0; j < image.Height - 1; j++)
                {
                    allPixels[i, j] = image.GetPixel(i, j);
                }
            }
            return allPixels;
        }

        private Color SearchMedianPixel(int x, int y, int size)
        {
            Color pixel;
            int[] arrayR = new int[size * size];
            int[] arrayG = new int[size * size];
            int[] arrayB = new int[size * size];
            int countPixel = 0;
            for (int i = x - size / 2; i < x + size / 2 + 1; i++)
            {
                for (int j = y - size / 2; j < y + size / 2 + 1; j++)
                {
                    arrayR[countPixel] = _allPixels[i, j].R;
                    arrayG[countPixel] = _allPixels[i, j].G;
                    arrayB[countPixel] = _allPixels[i, j].B;
                    countPixel++;
                }
            }
            Array.Sort(arrayR);
            Array.Sort(arrayG);
            Array.Sort(arrayB);
            pixel = Color.FromArgb(arrayR[size * size / 2], arrayG[size * size / 2], arrayB[size * size / 2]);
            return pixel;
        }

        private float Px(int init, int end, int[] hist)
        {
            int sum = 0;
            int i;
            for (i = init; i <= end; i++)
                sum += hist[i];

            return (float)sum;
        }

        // function is used to compute the mean values in the equation (mu)
        private float Mx(int init, int end, int[] hist)
        {
            int sum = 0;
            int i;
            for (i = init; i <= end; i++)
                sum += i * hist[i];

            return (float)sum;
        }

        // finds the maximum element in a vector
        private int findMax(float[] vec, int n)
        {
            float maxVec = 0;
            int idx = 0;
            int i;

            for (i = 1; i < n - 1; i++)
            {
                if (vec[i] > maxVec)
                {
                    maxVec = vec[i];
                    idx = i;
                }
            }
            return idx;
        }

        // simply computes the image histogram
        unsafe private void getHistogram(byte* p, int w, int h, int ws, int[] hist)
        {
            hist.Initialize();
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w * 3; j += 3)
                {
                    int index = i * ws + j;
                    hist[p[index]]++;
                }
            }
        }

        // find otsu threshold
        public int getOtsuThreshold(Bitmap bmp)
        {
            byte t = 0;
            float[] vet = new float[256];
            int[] hist = new int[256];
            vet.Initialize();

            float p1, p2, p12;
            int k;

            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();

                getHistogram(p, bmp.Width, bmp.Height, bmData.Stride, hist);

                // loop through all possible t values and maximize between class variance
                for (k = 1; k != 255; k++)
                {
                    p1 = Px(0, k, hist);
                    p2 = Px(k + 1, 255, hist);
                    p12 = p1 * p2;
                    if (p12 == 0)
                        p12 = 1;
                    float diff = (Mx(0, k, hist) * p2) - (Mx(k + 1, 255, hist) * p1);
                    vet[k] = (float)diff * diff / p12;

                }
            }
            bmp.UnlockBits(bmData);

            t = (byte)findMax(vet, 256);

            return t;
        }

        // simple routine to convert to gray scale
        public void Convert2GrayScaleFast(Bitmap bmp)
        {
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();
                int stopAddress = (int)p + bmData.Stride * bmData.Height;
                while ((int)p != stopAddress)
                {
                    p[0] = (byte)(.299 * p[2] + .587 * p[1] + .114 * p[0]);
                    p[1] = p[0];
                    p[2] = p[0];
                    p += 3;
                }
            }
            bmp.UnlockBits(bmData);
        }

        // simple routine for thresholdin
        public void threshold(Bitmap bmp, int thresh)
        {
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();
                int h = bmp.Height;
                int w = bmp.Width;
                int ws = bmData.Stride;

                for (int i = 0; i < h; i++)
                {
                    byte* row = &p[i * ws];
                    for (int j = 0; j < w * 3; j += 3)
                    {
                        row[j] = (byte)((row[j] > (byte)thresh) ? 255 : 0);
                        row[j + 1] = (byte)((row[j + 1] > (byte)thresh) ? 255 : 0);
                        row[j + 2] = (byte)((row[j + 2] > (byte)thresh) ? 255 : 0);
                    }
                }
            }
            bmp.UnlockBits(bmData);
        }
        ///////////////////////////////////////////////
    }
}
