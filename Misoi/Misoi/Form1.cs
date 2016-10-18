using System;
using System.Drawing;
using System.Windows.Forms;
using Filtering;
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
            FilteredImage.Image = filters.FiterPicture(_originalPicture);
        }
    }
}
