using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace Perceptron
{
    public interface IRecognator
    {
        char RecognateLetter(Bitmap letterBitmap);
    }
}