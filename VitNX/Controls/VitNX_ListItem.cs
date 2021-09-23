﻿using VitNX.Config;
using System;
using System.Drawing;

namespace VitNX.Controls
{
    public class VitNXListItem
    {
        #region Event Region

        public event EventHandler TextChanged;

        #endregion

        #region Field Region

        private string _text;

        #endregion

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

        #endregion

        #region Constructor Region

        public VitNXListItem()
        {
            TextColor = Colors.LightText;
            FontStyle = FontStyle.Regular;
        }

        public VitNXListItem(string text)
            : this()
        {
            Text = text;
        }

        #endregion
    }
}