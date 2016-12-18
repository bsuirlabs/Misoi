using System.Drawing;
using System.Drawing.Imaging;

namespace Perceptron
{
    public class Teacher:ITeacher
    {
        public void Teach(Helpers.Neuron[] neuroWeb, char s)
        {
            Bitmap patternImage = new Bitmap("D:\\temp\\patterns\\" + s + ".jpg");
            for (int i = 0; i < 26; i++)
            {
                if (neuroWeb[i].Name == s)
                {
                    for (int x = 0; x < 60; x++)
                    {
                        for (int y = 0; y < 120; y++)
                        {
                            patternImage.SetPixel(x,y,neuroWeb[i].Memory[x,y]);
                        }
                    }
                    patternImage.Save("D:\\temp\\patterns\\" + s + ".jpg", ImageFormat.Jpeg);
                }
            }
        }
    }
}