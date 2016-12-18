using System.Collections.Generic;
using System.Drawing;

namespace Segmentation
{
    public interface ISegmentation
    {
        Bitmap ExecuteSegmentation(Bitmap inputPicture, double topK, double botK, double leftK, double rightK);

        List<Bitmap> GetLetters(Bitmap image);
    }
}
