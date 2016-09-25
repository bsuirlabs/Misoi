using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misoi.Image_Processing
{
    class Starter
    {
        private readonly IProcessImage processImage;

        public Starter(IProcessImage processImage)
        {
            this.processImage = processImage;
        }

        public void Start(string filePath)
        {
            processImage.Start(filePath);
        }
    }
}
