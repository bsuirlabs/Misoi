using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Filtering;
using Perceptron;
using Segmentation;
using Skewing;
using Filtering = Filtering.ApplyFilters;

namespace Misoi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap _originalPicture { get; set; }

        private Bitmap _finalPicture { get; set; }

        private void OpenFileBtn_Click(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void LoadImage()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePathTB.Text = openFileDialog.FileName;
                Image image = Image.FromFile(FilePathTB.Text);
                mainImage.Image = ScaleImage(image, mainImage.Width, mainImage.Height);
                _originalPicture = new Bitmap(mainImage.Image);
            }
        }

        private Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double) maxWidth/image.Width;
            var ratioY = (double) maxHeight/image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int) (image.Width*ratio);
            var newHeight = (int) (image.Height*ratio);
            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            IFiltering filters = new ApplyFilters();
            ISkew skew = new Skew();
            ISegmentation segmentation = new ApplySegmentation();
            var filteredImage = filters.FiterPicture(_originalPicture, false, true, Int32.Parse(levelTB.Text), Int32.Parse(windowsSizeTB.Text));
            //textBox1.Text = skew.ExecuteSkewing(s, int.Parse(textBox3.Text)).ToString();
            //FilteredImage.Image = skew.ExecuteSkewing(_originalPicture, filteredImage, 2);
            var letters = segmentation.GetLetters(filteredImage);
            var count = 0;
            var resizedLetters = new List<Bitmap>();
            foreach (var letter in letters)
            {
                var resizedLetter = ScaleImage(letter, 60, 120);
                count++;
                resizedLetters.Add(resizedLetter);
                var path = "D:\\temp\\res\\" + count.ToString() + ".jpg";
                resizedLetter.Save(path, ImageFormat.Jpeg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //_originalPicture = new Bitmap(mainImage.Image);
            //ISegmentation segmentation = new ApplySegmentation();
            //FilteredImage.Image = FilteredImage.InitialImage;
            //FilteredImage.Image = segmentation.ExecuteSegmentation(_originalPicture, double.Parse(textBox1.Text), double.Parse(textBox2.Text), double.Parse(textBox3.Text), double.Parse(textBox4.Text));
            //var s = resizePixels(_originalPicture, 60, 120);
            IRecognator recognator = new Recognator();
            var s = new Bitmap("D:\\temp\\1.jpg");
            recognator.RecognateLetter(s);
        }

        private Bitmap ScaleImage(Bitmap orImage, int maxWidth, int maxHeight)
        {
            var image = new Bitmap(orImage);
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        private Bitmap resizePixels(Bitmap image, int w2, int h2)
        {
            Bitmap temp = new Bitmap(_originalPicture, w2, h2);
            var w1 = image.Width;
            var h1 = image.Height;
            //int[w2 * h2];
            // EDIT: added +1 to account for an early rounding problem
            int x_ratio = (int)((w1 << 16) / w2) + 1;
            int y_ratio = (int)((h1 << 16) / h2) + 1;
            //int finalRatio = x_ratio > y_ratio ? x_ratio : y_ratio;
            //int x_ratio = (int)((w1<<16)/w2) ;
            //int y_ratio = (int)((h1<<16)/h2) ;
            int x2, y2;
            for (int i = 0; i < h2; i++)
            {
                for (int j = 0; j < w2; j++)
                {
                    x2 = ((j * x_ratio) >> 16);
                    y2 = ((i * y_ratio) >> 16);
                    temp.SetPixel(j, i, _originalPicture.GetPixel(x2, y2));

                    //temp[(i * w2) + j] = pixels[(y2 * w1) + x2];
                }
            }
            return temp;
        }
    }
}
