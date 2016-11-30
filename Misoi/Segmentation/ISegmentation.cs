using System.Drawing;

namespace Segmentation
{
    public interface ISegmentation
    {
        Bitmap ExecuteSegmentation(Bitmap inputPicture, double topK, double botK, double leftK, double rightK);
    }
}
