using System;
using System.Drawing;

namespace VitNX.UI.ControlsV2
{
    public class Helper
    {
        public static int MyTextBoxesFocus(bool enter)
        {
            if (enter) return 2;
            else return 1;
        }

        public static void MyButton_MouseLeave(object sender, EventArgs e)
        {
            MyButton temp = sender as MyButton;
            temp.BorderColor = Color.FromArgb(26, 32, 48);
        }

        public static void MyButton_MouseEnter(object sender, EventArgs e)
        {
            MyButton temp = sender as MyButton;
            temp.BorderColor = Color.FromArgb(45, 50, 65);
        }
    }
}