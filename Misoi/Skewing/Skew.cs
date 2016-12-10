using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Skewing
{
    public class Skew : ISkew
    {
        private int _windowSize = 3;
        public Bitmap ExecuteSkewing(Bitmap originalImage, Bitmap filteredImage, int counter)
        {
            var newImage = BlurText(filteredImage, counter);
            var angleToRotate = GetAngleToRotate(newImage);
            Bitmap rotatedResult = new Bitmap(originalImage);
            RotateImage(rotatedResult, angleToRotate);

            return rotatedResult;
        }

        private void RotateImage(Bitmap image, float angle)
        {
            Graphics g = Graphics.FromImage(image);
            Matrix m = new Matrix();
            m.RotateAt(angle, new Point(image.Width/2, image.Height/2));
            g.Transform = m;
            g.DrawImage(image, 0, 0, image.Width, image.Height);
        }

        private Bitmap BlurText(Bitmap originalImage, int counter)
        {
            var tempImage = new Bitmap(originalImage);
            for (int i = 0; i < counter; i++)
            {
                var newImage = new Bitmap(tempImage);
                for (int x = 1; x < tempImage.Width - 1; x++)
                {
                    for (int y = 1; y < tempImage.Height - 1; y++)
                    {
                        if (tempImage.GetPixel(x, y).R == 0 && tempImage.GetPixel(x, y).G == 0 &&
                            tempImage.GetPixel(x, y).B == 0)
                        {
                            newImage.SetPixel(x - 1, y - 1, Color.Black);
                            newImage.SetPixel(x, y - 1, Color.Black);
                            newImage.SetPixel(x + 1, y - 1, Color.Black);
                            newImage.SetPixel(x - 1, y, Color.Black);
                            newImage.SetPixel(x, y, Color.Black);
                            newImage.SetPixel(x + 1, y, Color.Black);
                            newImage.SetPixel(x - 1, y + 1, Color.Black);
                            newImage.SetPixel(x, y + 1, Color.Black);
                            newImage.SetPixel(x + 1, y + 1, Color.Black);
                        }
                    }
                }
                tempImage = newImage;
            }
            return tempImage;
        }

        private float GetAngleToRotate(Bitmap image)
        {
            var maxRange = 0;
            int[,] houghMatrix = new int[180,(int)(Math.Sqrt(image.Width* image.Width+ image.Height* image.Height))];
            for (int x = 1; x < image.Width-1; x++)
            {
                for (int y = 1; y < image.Height-1; y++)
                {
                    if (PartOfEdge(x, y, image))
                    {
                        for (int i = 0; i < 180; i++)
                        {
                            maxRange = Math.Abs((int)(x*Math.Cos(i*Math.PI/180) + y*Math.Sin(i*Math.PI/180)));
                            houghMatrix[i, maxRange]++;
                        }
                    }
                }
            }

            int t = 0;
            int angle = 5;
            for (int x = 0; x < 180; x++)
            {
                for (int y = 0; y < (int) (Math.Sqrt(image.Width*image.Width + image.Height*image.Height)); y++)
                {
                    if (t < houghMatrix[x, y])
                    {
                        t = houghMatrix[x, y];
                        angle = x;
                    }
                }
            }
            return 90-angle;
        }

        private bool PartOfEdge(int x, int y, Bitmap image)
        {
            return image.GetPixel(x, y) != image.GetPixel(x - 1, y - 1) ||
                   image.GetPixel(x, y) != image.GetPixel(x, y - 1) ||
                   image.GetPixel(x, y) != image.GetPixel(x + 1, y - 1) ||
                   image.GetPixel(x, y) != image.GetPixel(x - 1, y) ||
                   image.GetPixel(x, y) != image.GetPixel(x + 1, y) ||
                   image.GetPixel(x, y) != image.GetPixel(x - 1, y + 1) ||
                   image.GetPixel(x, y) != image.GetPixel(x, y + 1) ||
                   image.GetPixel(x, y) != image.GetPixel(x + 1, y + 1);
        }

        //private double GetLuminance(Color pixel)
        //{
        //    return pixel.R*0.2126 + pixel.G*0.7152 + pixel.B*0.0722;
        //}

        //private double[,] GetTable()
        //{
        //    double[,] table = new double[2, 181]; // 0 - cos, 1 - sin;
        //    double rad = Math.PI / 180;
        //    double theta = rad * -90;
        //    for (int i = 0; i < 181; i++)
        //    {
        //        table[0, i] = Math.Cos(theta);
        //        table[1, i] = Math.Sin(theta);
        //        theta += rad;
        //    }
        //    return table;
        //}
    }
}
