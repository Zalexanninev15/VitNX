using System;
using System.Drawing;
using System.Windows.Forms;

using VitNX3.Functions.Win32;

namespace VitNX2.UI.ControlsV2
{
    public partial class VitNX2_MessageBox_Form : Form
    {
        private Color primaryColor = Color.FromArgb(21, 29, 38);
        private int borderSize = 2;

        public Color PrimaryColor
        {
            get { return primaryColor; }
            set
            {
                primaryColor = value;
                BackColor = primaryColor;
                windowTitle.BackColor = PrimaryColor;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(new Point(0, 0), new Size(Width, Height));
            Pen pen = new Pen(Color.FromArgb(26, 32, 48));
            g.DrawRectangle(pen, rect);
        }

        public VitNX2_MessageBox_Form(string text)
        {
            InitializeComponent();
            InitializeItems();
            PrimaryColor = primaryColor;
            labelMessage.Text = text;
            labelCaption.Text = "";
            SetFormSize();
            VitNX3.Functions.WindowAndControls.Window.SetWindowsElevenStyleForWinForm(Handle, Width, Height);
        }

        public VitNX2_MessageBox_Form(string text, string caption)
        {
            InitializeComponent();
            InitializeItems();
            PrimaryColor = primaryColor;
            labelMessage.Text = text;
            labelCaption.Text = caption;
            SetFormSize();
            SetButtons(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);
        }

        public VitNX2_MessageBox_Form(string text,
            string caption,
            MessageBoxButtons buttons)
        {
            InitializeComponent();
            InitializeItems();
            PrimaryColor = primaryColor;
            labelMessage.Text = text;
            labelCaption.Text = caption;
            SetFormSize();
            SetButtons(buttons, MessageBoxDefaultButton.Button1);
        }

        public VitNX2_MessageBox_Form(string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            InitializeComponent();
            InitializeItems();
            PrimaryColor = primaryColor;
            labelMessage.Text = text;
            labelCaption.Text = caption;
            SetFormSize();
            SetButtons(buttons, MessageBoxDefaultButton.Button1);
            SetIcon(icon);
        }

        public VitNX2_MessageBox_Form(string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton)
        {
            InitializeComponent();
            InitializeItems();
            PrimaryColor = primaryColor;
            labelMessage.Text = text;
            labelCaption.Text = caption;
            SetFormSize();
            SetButtons(buttons, defaultButton);
            SetIcon(icon);
        }

        private void InitializeItems()
        {
            FormBorderStyle = FormBorderStyle.None;
            Padding = new Padding(borderSize);
            labelMessage.MaximumSize = new Size(550, 0);
            button1.DialogResult = DialogResult.OK;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
        }

        private void SetFormSize()
        {
            int widht = labelMessage.Width + pictureBoxIcon.Width + panelBody.Padding.Left;
            int height = windowTitle.Height + labelMessage.Height + panelButtons.Height + panelBody.Padding.Top;
            Size = new Size(widht, height);
        }

        private void SetButtons(MessageBoxButtons buttons,
            MessageBoxDefaultButton defaultButton)
        {
            int xCenter = (panelButtons.Width - button1.Width) / 2;
            int yCenter = (panelButtons.Height - button1.Height) / 2;
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    {
                        button1.Visible = true;
                        button1.Location = new Point(xCenter, yCenter);
                        button1.Text = "OK";
                        button1.DialogResult = DialogResult.OK;
                        SetDefaultButton(defaultButton);
                        break;
                    }
                case MessageBoxButtons.OKCancel:
                    {
                        button1.Visible = true;
                        button1.Location = new Point(xCenter - (button1.Width / 2) - 5, yCenter);
                        button1.Text = "OK";
                        button1.DialogResult = DialogResult.OK;
                        button2.Visible = true;
                        button2.Location = new Point(xCenter + (button2.Width / 2) + 5, yCenter);
                        button2.Text = "Cancel";
                        button2.DialogResult = DialogResult.Cancel;
                        button2.BackColor = Color.OrangeRed;
                        if (defaultButton != MessageBoxDefaultButton.Button3)
                            SetDefaultButton(defaultButton);
                        else
                            SetDefaultButton(MessageBoxDefaultButton.Button1);
                        break;
                    }
                case MessageBoxButtons.RetryCancel:
                    {
                        button1.Visible = true;
                        button1.Location = new Point(xCenter - (button1.Width / 2) - 5, yCenter);
                        button1.Text = "Retry";
                        button1.DialogResult = DialogResult.Retry;
                        button2.Visible = true;
                        button2.Location = new Point(xCenter + (button2.Width / 2) + 5, yCenter);
                        button2.Text = "Cancel";
                        button2.DialogResult = DialogResult.Cancel;
                        button2.BackColor = Color.OrangeRed;
                        if (defaultButton != MessageBoxDefaultButton.Button3)
                            SetDefaultButton(defaultButton);
                        else
                            SetDefaultButton(MessageBoxDefaultButton.Button1);
                        break;
                    }
                case MessageBoxButtons.YesNo:
                    {
                        button1.Visible = true;
                        button1.Location = new Point(xCenter - (button1.Width / 2) - 5, yCenter);
                        button1.Text = "Yes";
                        button1.DialogResult = DialogResult.Yes;
                        button2.Visible = true;
                        button2.Location = new Point(xCenter + (button2.Width / 2) + 5, yCenter);
                        button2.Text = "No";
                        button2.DialogResult = DialogResult.No;
                        button2.BackColor = Color.IndianRed;
                        if (defaultButton != MessageBoxDefaultButton.Button3)
                            SetDefaultButton(defaultButton);
                        else
                            SetDefaultButton(MessageBoxDefaultButton.Button1);
                        break;
                    }
                case MessageBoxButtons.YesNoCancel:
                    {
                        button1.Visible = true;
                        button1.Location = new Point(xCenter - button1.Width - 5, yCenter);
                        button1.Text = "Yes";
                        button1.DialogResult = DialogResult.Yes;
                        button2.Visible = true;
                        button2.Location = new Point(xCenter, yCenter);
                        button2.Text = "No";
                        button2.DialogResult = DialogResult.No;
                        button2.BackColor = Color.IndianRed;
                        button3.Visible = true;
                        button3.Location = new Point(xCenter + button2.Width + 5, yCenter);
                        button3.Text = "Cancel";
                        button3.DialogResult = DialogResult.Cancel;
                        button3.BackColor = Color.OrangeRed;
                        SetDefaultButton(defaultButton);
                        break;
                    }
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        button1.Visible = true;
                        button1.Location = new Point(xCenter - button1.Width - 5, yCenter);
                        button1.Text = "Abort";
                        button1.DialogResult = DialogResult.Abort;
                        button1.BackColor = Color.Goldenrod;
                        button2.Visible = true;
                        button2.Location = new Point(xCenter, yCenter);
                        button2.Text = "Retry";
                        button2.DialogResult = DialogResult.Retry;
                        button3.Visible = true;
                        button3.Location = new Point(xCenter + button2.Width + 5, yCenter);
                        button3.Text = "Ignore";
                        button3.DialogResult = DialogResult.Ignore;
                        button3.BackColor = Color.IndianRed;
                        SetDefaultButton(defaultButton);
                        break;
                    }
            }
        }

        private void SetDefaultButton(MessageBoxDefaultButton defaultButton)
        {
            switch (defaultButton)
            {
                case MessageBoxDefaultButton.Button1:
                    {
                        button1.Select();
                        button1.ForeColor = Color.White;
                        button1.Font = new Font(button1.Font, FontStyle.Regular);
                        break;
                    }
                case MessageBoxDefaultButton.Button2:
                    {
                        button2.Select();
                        button2.ForeColor = Color.White;
                        button2.Font = new Font(button2.Font, FontStyle.Regular);
                        break;
                    }
                case MessageBoxDefaultButton.Button3:
                    {
                        button3.Select();
                        button3.ForeColor = Color.White;
                        button3.Font = new Font(button3.Font, FontStyle.Regular);
                        break;
                    }
            }
        }

        private void SetIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Error:
                    {
                        pictureBoxIcon.Image = VitNX.Properties.Resources.Error1;
                        //PrimaryColor = Color.FromArgb(224, 79, 95);
                        break;
                    }
                case MessageBoxIcon.Information:
                    {
                        pictureBoxIcon.Image = VitNX.Properties.Resources.Information;
                        //PrimaryColor = Color.FromArgb(38, 191, 166);
                        break;
                    }
                case MessageBoxIcon.Question:
                    {
                        pictureBoxIcon.Image = VitNX.Properties.Resources.Question1;
                        //PrimaryColor = Color.FromArgb(10, 119, 232);
                        break;
                    }
                case MessageBoxIcon.Exclamation:
                    {
                        pictureBoxIcon.Image = VitNX.Properties.Resources.Warning1;
                        //PrimaryColor = Color.FromArgb(255, 140, 0);
                        break;
                    }
                case MessageBoxIcon.None:
                    {
                        pictureBoxIcon.Image = VitNX.Properties.Resources.News;
                        //PrimaryColor = Color.CornflowerBlue;
                        break;
                    }
            }
        }

        private void titleExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void windowTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Import.ReleaseCapture();
            Import.PostMessage(Handle,
                Constants.WM_SYSCOMMAND,
                Constants.DOMOVE, 0);
        }

        private void titleExit_MouseEnter(object sender, EventArgs e)
        {
            titleExit.ForeColor = Color.Red;
        }

        private void titleExit_MouseLeave(object sender, EventArgs e)
        {
            titleExit.ForeColor = Color.FromArgb(183, 185, 191);
        }
    }
}