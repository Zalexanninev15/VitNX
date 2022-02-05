using System;
using System.Drawing;

namespace VitNX.Functions.Common
{
    public class CShap
    {
        public static void CleanMemory()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch { }
        }

        public static Color ConvertDWordColorToRGB(uint colorSetEx)
        {
            byte redColor = (byte)((0x000000FF & colorSetEx) >> 0);
            byte greenColor = (byte)((0x0000FF00 & colorSetEx) >> 8);
            byte blueColor = (byte)((0x00FF0000 & colorSetEx) >> 16);
            return Color.FromArgb(redColor, greenColor, blueColor);
        }
    }
}