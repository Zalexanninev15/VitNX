using System;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX.UI.OtherControls
{
    public partial class NotificationForm : Form
    {
        public NotificationForm()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        public Manager manager;
        private bool wasClosed = false, state = true;

        private enum Action
        {
            wait,
            start,
            close
        }

        private Action action;

        private int x, y;

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (action)
            {
                case Action.wait:
                    timer1.Interval = manager.WaitingTime;
                    action = Action.close;
                    break;

                case Action.start:
                    timer1.Interval = manager.TimerInterval;
                    Opacity += 0.1;
                    if (Opacity == 1.0)
                        action = Action.wait;
                    else
                    {
                        switch (manager.PositionType)
                        {
                            case NotificationPosition.Right:
                                Left--;
                                break;

                            case NotificationPosition.Left:
                                Left++;
                                break;

                            case NotificationPosition.Middle:
                                Top += manager.InvertAdding ? 1 : -1;
                                break;
                        }
                    }
                    break;

                case Action.close:
                    timer1.Interval = manager.TimerInterval;
                    Opacity -= 0.1;
                    switch (manager.PositionType)
                    {
                        case NotificationPosition.Right:
                            Left -= 3;
                            break;

                        case NotificationPosition.Left:
                            Left += 3;
                            break;

                        case NotificationPosition.Middle:
                            Top += manager.InvertAdding ? 3 : -3;
                            break;
                    }
                    if (Opacity == 0.0)
                    {
                        if (manager.PositionType == NotificationPosition.Right)
                            NotifySettings.right--;
                        if (manager.PositionType == NotificationPosition.Left)
                            NotifySettings.left--;
                        if (manager.PositionType == NotificationPosition.Middle)
                            NotifySettings.middle--;

                        foreach (var frm in Application.OpenForms)
                        {
                            if (frm.GetType() != GetType())
                                continue;
                            if (manager.EnableOffset && frm != null && ((NotificationForm)frm).manager.PositionType == manager.PositionType && !frm.Equals(this))
                                ((NotificationForm)frm).ChangePosition();
                        }
                        if (wasClosed)
                            manager.onClose(sender, e);
                        else
                            manager.onFinish(sender, e);
                        Close();
                    }
                    break;
            }
        }

        public void showAlert(string msg, NotificationType type, Manager notify)
        {
            manager = notify;
            lblMsg.Font = notify.Font;
            button1.FlatAppearance.MouseOverBackColor = button1.FlatAppearance.MouseDownBackColor =
                button1.FlatAppearance.BorderColor = notify.HasHighlighting ? Color.Empty : button1.BackColor;
            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;
            int Count = 1;
            bool? isInverted = null;
            bool isDisposed = false;
            foreach (var frm in Application.OpenForms)
            {
                if (frm.GetType() != GetType())
                    continue;
                if (frm != null && ((NotificationForm)frm).manager.PositionType == manager.PositionType && !frm.Equals(this))
                {
                    if (isInverted == null)
                        isInverted = ((NotificationForm)frm).manager.InvertAdding;
                    else if (((NotificationForm)frm).manager.InvertAdding != isInverted || isInverted != manager.InvertAdding)
                    {
                        if (manager.PositionType == NotificationPosition.Right)
                            NotifySettings.right--;
                        if (manager.PositionType == NotificationPosition.Left)
                            NotifySettings.left--;
                        if (manager.PositionType == NotificationPosition.Middle)
                            NotifySettings.middle--;
                        isDisposed = true;
                        Close();
                    }
                    Count++;
                }
            }
            Name = "alert" + (Count + 1).ToString();
            lblMsg.Text = msg;
            int delta = lblMsg.Width - 225;
            if (lblMsg.Width > 225)
            {
                if (lblMsg.Width >= manager.MaxTextWidth)
                {
                    delta = manager.MaxTextWidth - 225;
                    lblMsg.AutoSize = false;
                    lblMsg.Width = manager.MaxTextWidth;
                }
                Width += delta;
                button1.Location = new Point(button1.Location.X + delta, button1.Location.Y);
            }

            switch (manager.PositionType)
            {
                case NotificationPosition.Right:
                    x = Functions.Common.Information.Monitor.WorkingArea.Width - Width - 10;
                    break;

                case NotificationPosition.Left:
                    x = 10;
                    break;

                case NotificationPosition.Middle:
                    x = (Functions.Common.Information.Monitor.WorkingArea.Width - Width) / 2;
                    break;
            }
            y = !manager.InvertAdding ? Functions.Common.Information.Monitor.WorkingArea.Height - Height * Count - 5 * Count : Height * Count + 5 * Count;
            Location = new Point(x, y);
            if (manager.PositionType == NotificationPosition.Right)
                NotifySettings.right++;
            if (manager.PositionType == NotificationPosition.Left)
                NotifySettings.left++;
            if (manager.PositionType == NotificationPosition.Middle)
                NotifySettings.middle++;
            x = Functions.Common.Information.Monitor.WorkingArea.Width - base.Width - 5;
            button1.Image = notify.Images.Cancel;
            switch (type)
            {
                case NotificationType.Success:
                    pictureBox1.Image = notify.Images.Success;
                    BackColor = notify.Colors.Success;
                    break;

                case NotificationType.Error:
                    pictureBox1.Image = notify.Images.Error;
                    BackColor = notify.Colors.Error;
                    break;

                case NotificationType.Info:
                    pictureBox1.Image = notify.Images.Info;
                    BackColor = notify.Colors.Info;
                    break;

                case NotificationType.Warning:
                    pictureBox1.Image = notify.Images.Warning;
                    BackColor = notify.Colors.Warning;
                    break;
            }
            if (!isDisposed)
            {
                Show();
                action = Action.start;
                timer1.Interval = notify.TimerInterval;
                timer1.Start();
            }
        }

        public void showAlert(string msg, NotificationType type, Color color, Image picture, Manager notify)
        {
            pictureBox1.Image = picture;
            BackColor = color;
            showAlert(msg, type, notify);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;
                return Params;
            }
        }

        private void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Opacity == 1.0)
            {
                timer1.Interval = manager.TimerInterval;
                action = Action.close;
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wasClosed = true;
            timer1.Interval = manager.TimerInterval;
            action = Action.close;
        }

        public void ChangePosition()
        {
            if (manager.InvertAdding ? Location.Y > Height + 5 : Location.Y < Functions.Common.Information.Monitor.WorkingArea.Height - Height - 5)
            {
                Timer timer = new Timer();
                timer.Tag = (manager.InvertAdding && Location.Y != Height + 5)
                    || (!manager.InvertAdding && Location.Y != Functions.Common.Information.Monitor.WorkingArea.Height - Height - 5) ? Height - 1 : 0;
                timer.Interval = 1;
                timer.Tick += ((se, evu) =>
                {
                    if ((int)timer.Tag > 0)
                    {
                        Location = new Point(Location.X, Location.Y - (manager.InvertAdding ? 6 : -6));
                        timer.Tag = (int)timer.Tag - 6;
                    }
                    else
                        timer.Stop();
                });
                if ((int)timer.Tag != 0)
                    Location = new Point(Location.X, Location.Y - (manager.InvertAdding ? 6 : -6));
                timer.Start();
            }
        }
    }
}