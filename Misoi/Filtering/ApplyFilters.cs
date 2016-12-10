using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
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
            var newPicture = new Bitmap(pictureToProceed);
            applyMedian = false;
            applyMonochrome = true;
            if (applyMedian)
            {
                newPicture = ApplyMedianFilter(newPicture, windowSize);
            }
            if (applyMonochrome)
            {
                newPicture = ApplyMonochrom(newPicture);
            }
            return newPicture;
        }

        #region Monochrome

        private Bitmap ApplyMonochrom(Bitmap inputPicture)
        {
            int level = getOtsuThreshold(inputPicture);
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
#endregion

        #region Median

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
        #endregion

        #region Otsu
        private int GetLuminance(Color input)
        {
            return (input.R*2 + input.G*3 + input.B)/6;
        }
        
        // find otsu threshold
        public int getOtsuThreshold(Bitmap bmp)
        {
            // Посчитаем минимальную и максимальную яркость всех пикселей
            int min = GetLuminance(bmp.GetPixel(0,0));
            int max = GetLuminance(bmp.GetPixel(0,0));

            for (int y = 1; y < bmp.Height-1; y++)
            {
                for (int x = 0; x < bmp.Width-1; x++)
                {
                    int value = GetLuminance(bmp.GetPixel(x,y));

                    if (value < min)
                        min = value;

                    if (value > max)
                        max = value;
                }
            }

            // Гистограмма будет ограничена снизу и сверху значениями min и max,
            // поэтому нет смысла создавать гистограмму размером 256 бинов
            int histSize = max - min + 1;
            int[] hist = new int[histSize];

            // Заполним гистограмму нулями
            for (int t = 0; t < histSize; t++)
                hist[t] = 0;

            // И вычислим высоту бинов
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    hist[GetLuminance(bmp.GetPixel(x, y)) - min]++;
                }
            }
            // Введем два вспомогательных числа:
            int m = 0; // m - сумма высот всех бинов, домноженных на положение их середины
            int n = 0; // n - сумма высот всех бинов
            for (int t = 0; t <= max - min; t++)
            {
                m += t * hist[t];
                n += hist[t];
            }

            float maxSigma = -1; // Максимальное значение межклассовой дисперсии
            int threshold = 0; // Порог, соответствующий maxSigma

            int alpha1 = 0; // Сумма высот всех бинов для класса 1
            int beta1 = 0; // Сумма высот всех бинов для класса 1, домноженных на положение их середины

            // Переменная alpha2 не нужна, т.к. она равна m - alpha1
            // Переменная beta2 не нужна, т.к. она равна n - alpha1

            // t пробегается по всем возможным значениям порога
            for (int t = 0; t < max - min; t++)
            {
                alpha1 += t * hist[t];
                beta1 += hist[t];

                // Считаем вероятность класса 1.
                float w1 = (float)beta1 / n;
                // Нетрудно догадаться, что w2 тоже не нужна, т.к. она равна 1 - w1

                // a = a1 - a2, где a1, a2 - средние арифметические для классов 1 и 2
                float a = (float)alpha1 / beta1 - (float)(m - alpha1) / (n - beta1);

                // Наконец, считаем sigma
                float sigma = w1 * (1 - w1) * a * a;

                // Если sigma больше текущей максимальной, то обновляем maxSigma и порог
                if (sigma > maxSigma)
                {
                    maxSigma = sigma;
                    threshold = t;
                }
            }

            // Не забудем, что порог отсчитывался от min, а не от нуля
            threshold += min;

            // Все, порог посчитан, возвращаем его наверх :)
            return threshold;
        }
#endregion
    }
}
