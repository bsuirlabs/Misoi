using System.Drawing;

namespace Perceptron
{
    public interface IHelper
    {
        Helpers.Neuron[] GetNeurons(Bitmap letter);
    }
}