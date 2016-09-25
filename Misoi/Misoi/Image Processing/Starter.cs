using System.Drawing;


namespace Misoi.Image_Processing
{
    class Starter
    {
        private readonly IProcessImage processImage;

        public Starter(IProcessImage processImage)
        {
            this.processImage = processImage;
        }

        public Bitmap Start(string filePath)
        {
            return processImage.Start(filePath);
        }
    }
}
