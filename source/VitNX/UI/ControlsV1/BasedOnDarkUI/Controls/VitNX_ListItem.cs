using System;
using System.Drawing;

using VitNX.UI.ControlsV1.BasedOnDarkUI.Config;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Controls
{
    public class VitNX_ListItem
    {
        public event EventHandler TextChanged;

        private string _text;

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
    }
}