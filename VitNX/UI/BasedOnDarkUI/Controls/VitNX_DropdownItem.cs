using System.Drawing;

namespace VitNX.UI.BasedOnDarkUI.Controls
{
    public class VitNX_DropdownItem
    {
        #region Property Region

        public string Text { get; set; }

        public Bitmap Icon { get; set; }

        #endregion Property Region

        #region Constructor Region

        public VitNX_DropdownItem()
        { }

        public VitNX_DropdownItem(string text)
        {
            Text = text;
        }

        public VitNX_DropdownItem(string text, Bitmap icon)
            : this(text)
        {
            Icon = icon;
        }

        #endregion Constructor Region
    }
}