using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Segmentation
{
    public class ApplySegmentation: ISegmentation
    {
        public Bitmap ExecuteSegmentation(Bitmap inputPicture, double topK=0.5, double botK=0.5, double leftK=0.5, double rightK=0.5)
        {
            // k - some value between 0<k<1

            var averageStringLuminance = GetAverageLuminanceForStrings(inputPicture);
            var averageImageLuminance = GetAverageLuminance(averageStringLuminance);
            var topThreshold = averageImageLuminance*topK;
            var botThreshold = averageImageLuminance*botK;
            var pairedStringBorders = GetPairedTopAndBotValues(topThreshold, botThreshold, averageStringLuminance);
            ExecuteWordSegmentation(inputPicture, pairedStringBorders, leftK, rightK);
            return TestStringSegmentation(inputPicture, pairedStringBorders);
        }

        #region WordSegmentation

        private void ExecuteWordSegmentation(Bitmap inputPicture, List<SegmentedString> pairedStringBorders, double leftK, double rightK)
        {
            var outputImage = inputPicture;
            var result = new List<SegmentedWord>();
            foreach (var pair in pairedStringBorders)
            {
                outputImage = BlurFilter(inputPicture, pair.topLine, pair.botLine);
                var averageColumnLuminance = GetAverageLiminance(outputImage, pair.topLine, pair.botLine);
                var averageColumnString = GetAverageColumnStringLuminance(averageColumnLuminance);
                var leftThreshold = averageColumnString * leftK;
                var rightThreshold = averageColumnString * rightK;
                var pairedWordBorders = GetSegmentedWords(averageColumnLuminance, leftThreshold, rightThreshold);
                TestWordSegmentation(inputPicture, pairedWordBorders, pair);
            }
        }

        private void TestWordSegmentation(Bitmap inputPicture, List<SegmentedWord> leftRightBorders,
            SegmentedString topBotBorder)
        {
            foreach (var pair in leftRightBorders)
            {
                for (int y = topBotBorder.topLine; y < topBotBorder.botLine; y++)
                {
                    inputPicture.SetPixel(pair.leftLine,y,Color.Blue);
                    inputPicture.SetPixel(pair.rightLine,y,Color.Chocolate);
                }
            }
        }

        private List<SegmentedWord> GetSegmentedWords(int[] averageColumnLuminance, double leftThreshold, double rightThreshold)
        {
            var pairedValues = new List<SegmentedWord>();
            var temp = new SegmentedWord();
            bool startFound = false;
            for (int x = 2; x < averageColumnLuminance.Length-4; x++)
            {
                if (AvailableForLeft(averageColumnLuminance, leftThreshold, x))
                {
                    temp.leftLine = x;
                    startFound = true;
                }
                if (startFound && AvailableForRight(averageColumnLuminance, rightThreshold, x))
                {
                    temp.rightLine = x;
                    pairedValues.Add(temp);
                    temp = new SegmentedWord();
                    startFound = false;
                }
            }
            return pairedValues;
        }

        private bool AvailableForLeft(int[] averageColumnLuminance, double leftThreshold, int index)
        {
            return averageColumnLuminance[index] > leftThreshold &&
                   averageColumnLuminance[index + 1] > leftThreshold &&
                   averageColumnLuminance[index - 1] < leftThreshold;
        }

        private bool AvailableForRight(int[] averageColumnLuminance, double rightThreshold, int index)
        {
            return averageColumnLuminance[index] < rightThreshold &&
                   averageColumnLuminance[index + 1] < rightThreshold &&
                   averageColumnLuminance[index + 2] < rightThreshold &&
                   averageColumnLuminance[index + 3] < rightThreshold &&
                   averageColumnLuminance[index + 4] < rightThreshold &&
                   averageColumnLuminance[index - 1] > rightThreshold &&
                   averageColumnLuminance[index - 2] > rightThreshold;
        }

        private int GetAverageColumnStringLuminance(int[] columnsAverageLuminance)
        {
            var sum = 0;
            foreach (var columnLuminance in columnsAverageLuminance)
            {
                sum += columnLuminance;
            }
            return sum/columnsAverageLuminance.Length;
        }

        private int[] GetAverageLiminance(Bitmap inputImage, int startY, int endY)
        {
            int[] averageColumnLuminance = new int[inputImage.Width];
            for (int x = 0; x < inputImage.Width; x++)
            {
                int sum = 0;
                for (int y = startY; y < endY; y++)
                {
                    sum += (inputImage.GetPixel(x, y).R * 2 + inputImage.GetPixel(x, y).G * 3 +
                            inputImage.GetPixel(x, y).B) / 6;
                }
                averageColumnLuminance[x] = sum/Math.Abs(endY - startY);
            }
            return averageColumnLuminance;
        }

        private Bitmap BlurFilter(Bitmap inputImage, int startY, int endY)
        {
            var outputImage = inputImage;
            for (int y = startY+1; y < endY-1; y++)
            {
                for (int x = 1; x < inputImage.Width-1; x++)
                {
                    if (inputImage.GetPixel(x, y).R == 0 && inputImage.GetPixel(x, y).G == 0 &&
                        inputImage.GetPixel(x, y).B == 0)
                    {
                        outputImage = SetSurroundingPixels(outputImage, x, y, Color.Black);
                    }
                }
            }
            return outputImage;
        }

        private Bitmap SetSurroundingPixels(Bitmap image, int x, int y, Color color)
        {
            image.SetPixel(x - 1, y - 1, color);
            image.SetPixel(x - 1, y, color);
            image.SetPixel(x - 1, y + 1, color);
            image.SetPixel(x, y - 1, color);
            image.SetPixel(x, y + 1, color);
            image.SetPixel(x + 1, y - 1, color);
            image.SetPixel(x + 1, y, color);
            image.SetPixel(x + 1, y + 1, color);
            return image;
        }

#endregion

        #region StringSegmentation
        private Bitmap TestStringSegmentation(Bitmap picture, List<SegmentedString> pairedBorders)
        {
            foreach (var pair in pairedBorders)
            {
                for (int x = 0; x < picture.Width; x++)
                {
                    picture.SetPixel(x,pair.topLine,Color.ForestGreen);
                    picture.SetPixel(x,pair.botLine,Color.Red);
                }
            }
            return picture;
        }

        private List<SegmentedString> GetPairedTopAndBotValues(double topTreshold, double botTreshold, int[] averageStrigLuminance)
        {
            var pairedValues = new List<SegmentedString>();
            var temp = new SegmentedString();
            bool startFound = false;
            for (int y = 2; y < averageStrigLuminance.Length-3; y++)
            {
                if (AvailableForBot(topTreshold, averageStrigLuminance, botTreshold, y))
                {
                    temp.topLine = y;
                    startFound = true;
                }
                if (startFound && AvailableForTop(topTreshold, averageStrigLuminance, botTreshold, y))
                {
                    temp.botLine = y;
                    pairedValues.Add(temp);
                    temp = new SegmentedString();
                    startFound = false;
                }
            }
            pairedValues = FixHeight(pairedValues);
            return pairedValues;
        }

        private List<SegmentedString> FixHeight(List<SegmentedString> pairs)
        {
            int min = 1000;
            foreach (var pair in pairs)
            {
                if ((pair.botLine - pair.topLine) < min)
                {
                    min = (pair.botLine - pair.topLine);
                }
            }

            foreach (var pair in pairs)
            {
                pair.topLine = pair.topLine - Convert.ToInt32(min*0.3);
                pair.botLine = pair.botLine + Convert.ToInt32(min*0.3);
            }
            return pairs;
        } 

        private bool AvailableForBot(double topTreshold, int[] averageStringLuminance, double botTreshold, int index)
        {
            return (averageStringLuminance[index] > topTreshold &&
                    averageStringLuminance[index + 1] < botTreshold) ||
                   (averageStringLuminance[index + 1] < botTreshold &&
                    averageStringLuminance[index + 2] < botTreshold &&
                    averageStringLuminance[index + 3] < botTreshold);
        }

        private bool AvailableForTop(double topTreshold, int[] averageStringLuminance, double botTreshold, int index)
        {
            return averageStringLuminance[index] > botTreshold &&
                   (averageStringLuminance[index - 1] < topTreshold &&
                    averageStringLuminance[index - 2] < topTreshold) &&
                   (averageStringLuminance[index + 1] > botTreshold &&
                    averageStringLuminance[index + 2] > botTreshold &&
                    averageStringLuminance[index + 3] > botTreshold);
        }

        private int GetAverageLuminance(int[] values)
        {
            int temp = 0;
            for (int y = 0; y < values.Length; y++)
            {
                temp += values[y];
            }
            return temp/values.Length;
        }

        private int[] GetAverageLuminanceForStrings(Bitmap image)
        {
            int[] averageStringLuminance = new int[image.Height];
            
            for (int y = 0; y < image.Height; y++)
            {
                int sum = 0;
                for (int x = 0; x < image.Width; x++)
                {
                    sum += (image.GetPixel(x, y).R*2 + image.GetPixel(x, y).G*3 +
                            image.GetPixel(x, y).B)/6;
                }
                averageStringLuminance[y] = sum/image.Width;
            }
            return averageStringLuminance;
        }
#endregion

        private class SegmentedString
        {
            internal int topLine { get; set; }
            internal int botLine { get; set; }     
        }

        private class SegmentedWord
        {
            internal int leftLine { get; set; }
            internal int rightLine { get; set; }
        }
    }
}
