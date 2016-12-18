using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Perceptron
{
    public class Recognator: IRecognator
    {
        public char RecognateLetter(Bitmap letterBitmap)
        {
            IHelper helper = new Helpers();
            var neuroWeb = helper.GetNeurons(letterBitmap);
            return RecognizeLetter(neuroWeb);
        }

        private char RecognizeLetter(Helpers.Neuron[] neuroWeb)
        {
            for (int i = 0; i < 52; i++)
            {
                for (int x = 0; x < 60; x++)
                {
                    for (int y = 0; y < 120; y++)
                    {
                        var n = neuroWeb[i].Memory[x, y].ToArgb();
                        var m = neuroWeb[i].Input[x, y].ToArgb();

                        if ((Math.Abs(m - n) < 120))
                        {
                            if (m < 250)
                            {
                                neuroWeb[i].Weight++;
                            }
                        }

                        if (m != 0)
                        {
                            if (m < 250)
                            {
                                n = ((n + (n + m) / 2) / 2);
                            }
                            neuroWeb[i].Memory[x, y] = Color.FromArgb(n);
                        }
                        else if (n != 0)
                        {
                            if (m < 250)
                            {
                                n = ((n + (n + m) / 2) / 2);
                            }
                        }
                        neuroWeb[i].Memory[x, y] = Color.FromArgb(n);
                    }
                }
            }
            return neuroWeb.OrderByDescending(neuron => neuron.Weight).First().Name;
        }
    }
}
