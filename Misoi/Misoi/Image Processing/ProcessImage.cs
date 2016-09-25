using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misoi.Image_Processing
{
    class ProcessImage : IProcessImage
    {
        private IPrepareImage PrepareImage;
        public ProcessImage(IPrepareImage PrepareImage)
        {
            this.PrepareImage = PrepareImage;
        }

        public void Start(string filePath)
        {
            var pixels = PrepareImage.GetPixels(filePath);
        }

    }
}
