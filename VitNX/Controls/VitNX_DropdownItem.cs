using System.Drawing;
using System.Windows.Forms;

namespace VitNX.Controls
{
    public class VNXDropdownItem
    {
        #region Property Region

        public string Text { get; set; }

        public Bitmap Icon { get; set; }

        #endregion

        #region Constructor Region

        public VNXDropdownItem()
        { }

        public VNXDropdownItem(string text)
        {
            Text = text;
        }

        public VNXDropdownItem(string text, Bitmap icon)
            : this(text)
        {
            Icon = icon;
        }

        #endregion
    }
}
