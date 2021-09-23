using System.Drawing;

namespace VitNX.Controls
{
    public class VitNXDropdownItem
    {
        #region Property Region

        public string Text { get; set; }

        public Bitmap Icon { get; set; }

        #endregion

        #region Constructor Region

        public VitNXDropdownItem()
        { }

        public VitNXDropdownItem(string text)
        {
            Text = text;
        }

        public VitNXDropdownItem(string text, Bitmap icon)
            : this(text)
        {
            Icon = icon;
        }

        #endregion
    }
}