using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.Forms
{
    public partial class VitNXMessageBox : VitNXDialog
    {
        [System.Runtime.InteropServices.DllImport("DwmApi")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        protected override void OnHandleCreated(EventArgs e) { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } }

        #region Field Region

        private string _message;
        private int _maximumWidth = 350;

        #endregion

        #region Property Region

        [Description("Determines the maximum width of the message box when it autosizes around the displayed message.")]
        [DefaultValue(350)]
        public int MaximumWidth
        {
            get { return _maximumWidth; }
            set
            {
                _maximumWidth = value;
                CalculateSize();
            }
        }

        #endregion

        #region Constructor Region

        public VitNXMessageBox()
        {
            InitializeComponent();
        }

        public VitNXMessageBox(string message, string title, VitNXMessageBoxIcon icon, VitNXDialogButton buttons)
            : this()
        {
            Text = title;
            _message = message;

            DialogButtons = buttons;
            SetIcon(icon);
        }

        public VitNXMessageBox(string message)
            : this(message, null, VitNXMessageBoxIcon.None, VitNXDialogButton.Ok)
        { }

        public VitNXMessageBox(string message, string title)
            : this(message, title, VitNXMessageBoxIcon.None, VitNXDialogButton.Ok)
        { }

        public VitNXMessageBox(string message, string title, VitNXDialogButton buttons)
            : this(message, title, VitNXMessageBoxIcon.None, buttons)
        { }

        public VitNXMessageBox(string message, string title, VitNXMessageBoxIcon icon)
            : this(message, title, icon, VitNXDialogButton.Ok)
        { }

        #endregion

        #region Static Method Region

        public static DialogResult ShowInfo(string message, string caption, VitNXDialogButton buttons = VitNXDialogButton.Ok)
        {
            return ShowDialog(message, caption, VitNXMessageBoxIcon.Information, buttons);
        }

        public static DialogResult ShowWarning(string message, string caption, VitNXDialogButton buttons = VitNXDialogButton.Ok)
        {
            return ShowDialog(message, caption, VitNXMessageBoxIcon.Warning, buttons);
        }

        public static DialogResult ShowError(string message, string caption, VitNXDialogButton buttons = VitNXDialogButton.Ok)
        {
            return ShowDialog(message, caption, VitNXMessageBoxIcon.Error, buttons);
        }

        public static DialogResult ShowQuestion(string message, string caption, VitNXDialogButton buttons = VitNXDialogButton.YesNo)
        {
            return ShowDialog(message, caption, VitNXMessageBoxIcon.Question, buttons);
        }

        private static DialogResult ShowDialog(string message, string caption, VitNXMessageBoxIcon icon, VitNXDialogButton buttons)
        {
            using (var dlg = new VitNXMessageBox(message, caption, icon, buttons))
            {
                var result = dlg.ShowDialog();
                return result;
            }
        }

        #endregion

        #region Method Region

        private void SetIcon(VitNXMessageBoxIcon icon)
        {
            switch (icon)
            {
                case VitNXMessageBoxIcon.None:
                    picIcon.Visible = false;
                    lblText.Left = 10;
                    break;
                case VitNXMessageBoxIcon.Information:
                    picIcon.Image = MessageBoxIcons.info;
                    break;
                case VitNXMessageBoxIcon.Warning:
                    picIcon.Image = MessageBoxIcons.warning;
                    break;
                case VitNXMessageBoxIcon.Error:
                    picIcon.Image = MessageBoxIcons.error;
                    break;
                case VitNXMessageBoxIcon.Question:
                    picIcon.Image = MessageBoxIcons.question;
                    break;
            }
        }

        private void CalculateSize()
        {
            var width = 260; var height = 124;

            // Reset form back to original size
            Size = new Size(width, height);

            lblText.Text = string.Empty;
            lblText.AutoSize = true;
            lblText.Text = _message;

            // Set the minimum dialog size to whichever is bigger - the original size or the buttons.
            var minWidth = Math.Max(width, TotalButtonSize + 15);

            // Calculate the total size of the message
            var totalWidth = lblText.Right + 25;

            // Make sure we're not making the dialog bigger than the maximum size
            if (totalWidth < _maximumWidth)
            {
                // Width is smaller than the maximum width.
                // This means we can have a single-line message box.
                // Move the label to accomodate this.
                width = totalWidth;
                lblText.Top = picIcon.Top + (picIcon.Height / 2) - (lblText.Height / 2);
            }
            else
            {
                // Width is larger than the maximum width.
                // Change the label size and wrap it.
                width = _maximumWidth;
                var offsetHeight = Height - picIcon.Height;
                lblText.AutoUpdateHeight = true;
                lblText.Width = width - lblText.Left - 25;
                height = offsetHeight + lblText.Height;
            }

            // Force the width to the minimum width
            if (width < minWidth)
                width = minWidth;

            // Set the new size of the dialog
            Size = new Size(width, height);
        }

        #endregion

        #region Event Handler Region

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CalculateSize();
        }

        #endregion
    }
}