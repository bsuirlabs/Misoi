using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Misoi.Image_Processing;

namespace Misoi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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
                mainImage.Height = image.Height;
                mainImage.Width = image.Width;
                mainImage.Image = image;
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            Starter starter = new Starter(new ProcessImage(new PrepareImage()));
            starter.Start(FilePathTB.Text);
        }
    }
}
