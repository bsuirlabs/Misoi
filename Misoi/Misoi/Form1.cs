using System;
using System.Drawing;
using System.Windows.Forms;
using Filtering;
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
            var s = filters.FiterPicture(_originalPicture, medianCB.Checked, monochromeCB.Checked, Int32.Parse(levelTB.Text), Int32.Parse(windowsSizeTB.Text));
            //textBox1.Text = skew.ExecuteSkewing(s, int.Parse(textBox3.Text)).ToString();
            FilteredImage.Image = skew.ExecuteSkewing(_originalPicture, s, 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _originalPicture = new Bitmap(mainImage.Image);
            ISegmentation segmentation = new ApplySegmentation();
            FilteredImage.Image = FilteredImage.InitialImage;
            FilteredImage.Image = segmentation.ExecuteSegmentation(_originalPicture, double.Parse(textBox1.Text), double.Parse(textBox2.Text), double.Parse(textBox3.Text), double.Parse(textBox4.Text));
        }
    }
}
