using System;
using System.Drawing;

namespace VitNX.UI.ControlsV2
{
    public class Helper
    {
        public static int VitNX2_TextBoxesFocus(bool enter)
        {
            if (enter)
                return 2;
            else
                return 1;
        }

        public static void VitNX2_Button_MouseLeave(object sender, EventArgs e)
        {
            VitNX2_Button temp = sender as VitNX2_Button;
            temp.BorderColor = Color.FromArgb(26, 32, 48);
        }

        public static void VitNX2_Button_MouseEnter(object sender, EventArgs e)
        {
            VitNX2_Button temp = sender as VitNX2_Button;
            temp.BorderColor = Color.FromArgb(45, 50, 65);
        }
    }
}