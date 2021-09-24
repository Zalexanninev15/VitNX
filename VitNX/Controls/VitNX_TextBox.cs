using VitNX.Config;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VitNX_TextBox : TextBox
    {
        #region Constructor Region

        public VitNX_TextBox()
        {
            BackColor = Colors.LightBackground;
            ForeColor = Colors.LightText;
            Padding = new Padding(2, 2, 2, 2);
            BorderStyle = BorderStyle.FixedSingle;
        }

        #endregion
    }
}