using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using VitNX3;

namespace VitNX.UI.ControlsV1.Forms
{
    public partial class VitNX_MessageBox : VitNX_Dialog
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            VitNX3.Functions.WindowAndControls.Window.SetWindowsTenAndHighStyleForWinFormTitleToDark(Handle);
        }

        private string _message;
        private int _maximumWidth = 350;

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

        public VitNX_MessageBox()
        {
            InitializeComponent();
        }

        public VitNX_MessageBox(string message,
            string title,
            VitNX_MessageBoxIcon icon,
            VitNX_DialogButton buttons)
            : this()
        {
            Text = title;
            _message = message;
            DialogButtons = buttons;
            SetIcon(icon);
        }

        public VitNX_MessageBox(string message)
            : this(message, null,
                  VitNX_MessageBoxIcon.None,
                  VitNX_DialogButton.Ok)
        { }

        public VitNX_MessageBox(string message,
            string title)
            : this(message, title,
                  VitNX_MessageBoxIcon.None,
                  VitNX_DialogButton.Ok)
        { }

        public VitNX_MessageBox(string message,
            string title,
            VitNX_DialogButton buttons)
            : this(message, title,
                  VitNX_MessageBoxIcon.None,
                  buttons)
        { }

        public VitNX_MessageBox(string message,
            string title,
            VitNX_MessageBoxIcon icon)
            : this(message,
                  title,
                  icon,
                  VitNX_DialogButton.Ok)
        { }

        /// <summary>
        /// Shows the information MessageBox.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>A DialogResult.</returns>
        public static DialogResult ShowInformation(string message,
            string caption = "Information",
            VitNX_DialogButton buttons = VitNX_DialogButton.Ok)
        {
            return ShowDialog(message,
                caption,
                VitNX_MessageBoxIcon.Information,
                buttons);
        }

        /// <summary>
        /// Shows the warning MessageBox.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>A DialogResult.</returns>
        public static DialogResult ShowWarning(string message,
            string caption = "Warning",
            VitNX_DialogButton buttons = VitNX_DialogButton.Ok)
        {
            return ShowDialog(message,
                caption,
                VitNX_MessageBoxIcon.Warning,
                buttons);
        }

        /// <summary>
        /// Shows the error MessageBox.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>A DialogResult.</returns>
        public static DialogResult ShowError(string message,
            string caption = "Error",
            VitNX_DialogButton buttons = VitNX_DialogButton.Ok)
        {
            return ShowDialog(message,
                caption,
                VitNX_MessageBoxIcon.Error,
                buttons);
        }

        /// <summary>
        /// Shows the question MessageBox.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>A DialogResult.</returns>
        public static DialogResult ShowQuestion(string message,
            string caption = "Question",
            VitNX_DialogButton buttons = VitNX_DialogButton.YesNo)
        {
            return ShowDialog(message,
                caption,
                VitNX_MessageBoxIcon.Question,
                buttons);
        }

        private static DialogResult ShowDialog(string message,
            string caption,
            VitNX_MessageBoxIcon icon,
            VitNX_DialogButton buttons)
        {
            using (var dlg = new VitNX_MessageBox(message,
                caption,
                icon,
                buttons))
            {
                var result = dlg.ShowDialog();
                return result;
            }
        }

        private void SetIcon(VitNX_MessageBoxIcon icon)
        {
            switch (icon)
            {
                case VitNX_MessageBoxIcon.None:
                    picIcon.Visible = false;
                    lblText.Left = 10;
                    break;

                case VitNX_MessageBoxIcon.Information:
                    picIcon.Image = MessageBoxIcons.info;
                    break;

                case VitNX_MessageBoxIcon.Warning:
                    picIcon.Image = MessageBoxIcons.warning;
                    break;

                case VitNX_MessageBoxIcon.Error:
                    picIcon.Image = MessageBoxIcons.error;
                    break;

                case VitNX_MessageBoxIcon.Question:
                    picIcon.Image = MessageBoxIcons.question;
                    break;

                case VitNX_MessageBoxIcon.Shield:
                    picIcon.Image = VitNX.Properties.Resources.Shield;
                    break;

                case VitNX_MessageBoxIcon.Help:
                    picIcon.Image = VitNX.Properties.Resources.Help;
                    break;
            }
        }

        private void CalculateSize()
        {
            var width = 260; var height = 124;
            Size = new Size(width, height);
            lblText.Text = string.Empty;
            lblText.AutoSize = true;
            lblText.Text = _message;
            var minWidth = Math.Max(width,
                TotalButtonSize + 15);
            var totalWidth = lblText.Right + 25;
            if (totalWidth < _maximumWidth)
            {
                width = totalWidth;
                lblText.Top = picIcon.Top + (picIcon.Height / 2) - (lblText.Height / 2);
            }
            else
            {
                width = _maximumWidth;
                var offsetHeight = Height - picIcon.Height;
                lblText.AutoUpdateHeight = true;
                lblText.Width = width - lblText.Left - 25;
                height = offsetHeight + lblText.Height;
            }
            if (width < minWidth)
                width = minWidth;
            Size = new Size(width, height);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CalculateSize();
        }
    }
}