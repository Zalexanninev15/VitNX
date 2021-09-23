using VitNX.Config;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VitNXTextBox : TextBox
    {
        #region Constructor Region

        public VitNXTextBox()
        {
            BackColor = Colors.LightBackground;
            ForeColor = Colors.LightText;
            Padding = new Padding(2, 2, 2, 2);
            BorderStyle = BorderStyle.FixedSingle;
        }

        #endregion
    }
}