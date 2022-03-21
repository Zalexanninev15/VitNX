using System;
using System.ComponentModel;
using System.Windows.Forms;

using VitNX.Functions.Win32;

namespace VitNX.UI.ControlsV2
{
    public class VitNX2_RichTextBox : RichTextBox
    {
        public VitNX2_RichTextBox()
        {
            MouseDown += new MouseEventHandler(this.RichTextBox_Custom_Mouse);
            MouseUp += new MouseEventHandler(this.RichTextBox_Custom_Mouse);
            base.ReadOnly = true;
            base.TabStop = false;
            Import.HideCaret(Handle);
        }

        protected override void OnGotFocus(EventArgs e)
        { Import.HideCaret(Handle); }

        protected override void OnEnter(EventArgs e)
        { Import.HideCaret(Handle); }

        [DefaultValue(true)]
        public new bool ReadOnly
        {
            get { return true; }
            set { }
        }

        [DefaultValue(false)]
        public new bool TabStop
        {
            get { return false; }
            set { }
        }

        private void RichTextBox_Custom_Mouse(object sender, MouseEventArgs e)
        { Import.HideCaret(Handle); }

        private void InitializeComponent()
        { Resize += new EventHandler(RichTextBox_Custom_Resize); }

        private void RichTextBox_Custom_Resize(object sender, EventArgs e)
        { Import.HideCaret(Handle); }
    }
}