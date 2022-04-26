using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace VitNX3.UI.OtherControls
{
    /// <summary>
    /// The settings manager of notification.
    /// </summary>
    public class Manager
    {
        /// <summary>
		/// Delay between changes in notification transparency.
		/// </summary>
        public int TimerInterval = 1;

        /// <summary>
		/// Notification time in a static state with 100% transparency.
		/// </summary>
        public int WaitingTime = 5000;

        /// <summary>
		/// Maximum number of notifications on the screen.
		/// </summary>
        public int MaxCount = 9;

        /// <summary>
		/// Notification text font.
		/// </summary>
        public Font Font = new Font("Century Gothic", 12);

        /// <summary>
        /// The colors of the standard notification types.
        /// </summary>
        public Colors Colors = new Colors();

        /// <summary>
		/// Images used in standard types of notifications.
		/// </summary>
        public Images Images = new Images();

        /// <summary>
		/// If true, the notifications are displayed from top to bottom on the screen.
		/// </summary>
        public bool InvertAdding = false;

        /// <summary>
		/// Notification display location on the screen.
		/// </summary>
        public NotificationPosition PositionType = NotificationPosition.Right;

        public System.EventHandler onClose = delegate { };
        public System.EventHandler onFinish = delegate { };

        /// <summary>
		/// Offset of notifications after closing the previous one.
		/// </summary>
        public bool EnableOffset = true;

        // Maximum notification text length in pixels
        public int MaxTextWidth = 225;

        /// <summary>
		/// If true, the notification button will be highlighted when you hover the mouse over it.
		/// </summary>
        public bool HasHighlighting = true;

        /// <summary>
		/// Display a new standard type notification on the screen.
		/// </summary>
        /// <param name="message">The main message displayed.</param>
		/// <param name="type">Notification type.</param>
		/// <returns> </returns>
        public void Alert(string message, NotificationType type)
        {
            int max = 0;
            switch (PositionType)
            {
                case NotificationPosition.Right:
                    max = NotifySettings.right;
                    break;

                case NotificationPosition.Left:
                    max = NotifySettings.left;
                    break;

                case NotificationPosition.Middle:
                    max = NotifySettings.middle;
                    break;
            }
            if (max < MaxCount)
            {
                NotificationForm frm = new NotificationForm();
                frm.showAlert(message, type, this);
            }
        }

        /// <summary>
		/// Display a new custom notification on the screen.
		/// </summary>
        /// <param name="message">Main message displayed.</param>
		/// <param name="type">Notification type. Mandatory is Custom.</param>
        /// <param name="color">Notification background color.</param>
        /// <param name="picture">The image displayed on the left.</param>
		/// <returns> </returns>
        public void Alert(string message, NotificationType type, Color color, Image picture)
        {
            if (NotifySettings.right < MaxCount)
            {
                NotificationForm frm = new NotificationForm();
                frm.showAlert(message,
                    type,
                    color,
                    picture,
                    this);
            }
        }

        /// <summary>
		/// Close all notifications on the screen.
		/// </summary>
        public void CloseAll()
        {
            string fname;
            for (int i = 0; i < MaxCount + 1; i++)
            {
                fname = "alert" + i.ToString();
                NotificationForm frm = (NotificationForm)Application.OpenForms[fname];
                if (frm != null)
                    frm.Close();
            }
        }

        /// <summary>
		/// Stop all application events for the allotted time.
		/// </summary>
        /// <param name="Milliseconds">Pause time in milliseconds.</param>
        /// returns> </returns>
        public void StopTimer(int Milliseconds)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < Milliseconds)
                Application.DoEvents();
        }
    }

    /// <summary>
    /// Type of notification displayed.
    /// </summary>
    public enum NotificationType
    {
        Success,
        Warning,
        Error,
        Info,
        Custom
    }

    public class Colors
    {
        public Color Success = Color.FromArgb(255, 38, 171, 99);
        public Color Error = Color.FromArgb(255, 171, 37, 54);
        public Color Info = Color.RoyalBlue;
        public Color Warning = Color.DarkOrange;
    }

    public class Images
    {
        public Image Success = Properties.Resources.successN;
        public Image Error = Properties.Resources.errorN;
        public Image Info = Properties.Resources.infoN;
        public Image Warning = Properties.Resources.warningN;
        public Image Cancel = Properties.Resources.cancelN;
    }

    /// <summary>
    /// Место отображения уведомлений на экране.
    /// </summary>
    public enum NotificationPosition
    {
        Right,
        Left,
        Middle
    }
}