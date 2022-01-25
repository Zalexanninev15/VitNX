using System;
using System.Drawing;
using VitNX.UI.BasedOnDarkUI.Config;

namespace VitNX.UI.BasedOnDarkUI.Controls
{
    public class VitNX_ListItem
    {
        #region Event Region

        public event EventHandler TextChanged;

        #endregion Event Region

        #region Field Region

        private string _text;

        #endregion Field Region

        #region Property Region

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;

                if (TextChanged != null)
                    TextChanged(this, new EventArgs());
            }
        }

        public Rectangle Area { get; set; }

        public Color TextColor { get; set; }

        public FontStyle FontStyle { get; set; }

        public Bitmap Icon { get; set; }

        public object Tag { get; set; }

        #endregion Property Region

        #region Constructor Region

        public VitNX_ListItem()
        {
            TextColor = Colors.LightText;
            FontStyle = FontStyle.Regular;
        }

        public VitNX_ListItem(string text)
            : this()
        {
            Text = text;
        }

        #endregion Constructor Region
    }
}