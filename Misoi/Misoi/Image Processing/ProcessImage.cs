using System.Drawing;

namespace Misoi.Image_Processing
{
    class ProcessImage : IProcessImage
    {
        private IPrepareImage PrepareImage;
        public ProcessImage(IPrepareImage PrepareImage)
        {
            this.PrepareImage = PrepareImage;
        }

        public Bitmap Start(string filePath)
        {
            Bitmap bitmap = new Bitmap(filePath);
            var filteredImage = PrepareImage.FilterImage(bitmap);
            return filteredImage;
        }



    }
}
