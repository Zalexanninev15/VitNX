using System;

namespace VitNX3.UI.Console.ProgressBar
{
    /// <summary>
    /// Console ProgressBar type 3.
    /// </summary>

    /// <summary>
    /// A simple Console progress bar.
    /// Example: https://gist.github.com/Zalexanninev15/937a043a0656d39e953e0132e5648925
    /// </summary>
    public class ProgressBar
    {
        /// <summary>
        /// Create a default progress bar.
        /// </summary>
        /// <remarks>
        /// The progress bar created this way can only updated by percent.
        /// </remarks>
        public ProgressBar()
        {
        }

        /// <summary>
        /// Create a progress bar with specified total number.
        /// </summary>
        /// <param name="total">Total number which indicates the 100% progress.</param>
        public ProgressBar(int total)
        {
            this.total = total;
        }

        /// <summary>
        /// Show the progress bar.
        /// </summary>
        public void Show()
        {
            if (enabled == false)
            {
                enabled = true;
                cursorTop = System.Console.CursorTop;
                System.Console.WriteLine("0%");
            }
        }

        /// <summary>
        /// Update the progress by one item.
        /// </summary>
        public void UpdateOnce()
        {
            Update(++count * 1.0 / total);
        }

        /// <summary>
        /// Update the progress bar by count.
        /// </summary>
        /// <param name="count">The processed item count.</param>
        public void Update(int count)
        {
            this.count = count;
            Update(count * 1.0 / total);
        }

        /// <summary>
        /// Update the progress by percent.
        /// </summary>
        /// <param name="percent">The processed percentage.</param>
        public void Update(double percent)
        {
            if (enabled == false)
                return;
            if (Math.Round(percent * 100) <= this.percentage)
                return;
            int originCursorTop = System.Console.CursorTop;
            int originCursorLeft = System.Console.CursorLeft;
            ConsoleColor originBackgroundColor = System.Console.BackgroundColor;
            ConsoleColor originForegroundColor = System.Console.ForegroundColor;
            int width = System.Console.WindowWidth - textWidth;
            percentage = (int)Math.Round(percent * 100);
            System.Console.SetCursorPosition(0, cursorTop);
            System.Console.Write(new string(' ', textWidth));
            System.Console.SetCursorPosition(0, cursorTop);
            System.Console.Write($"{percentage}%");
            System.Console.BackgroundColor = originForegroundColor;
            int newCursorLeft = (int)Math.Round(percent * width);
            for (int cursor = cursorLeft; cursor < newCursorLeft; cursor++)
            {
                System.Console.SetCursorPosition(textWidth + cursor, cursorTop);
                System.Console.Write(' ');
            }
            System.Console.BackgroundColor = originBackgroundColor;
            System.Console.ForegroundColor = originForegroundColor;
            System.Console.CursorTop = originCursorTop;
            System.Console.CursorLeft = originCursorLeft;
            cursorLeft = newCursorLeft;
        }

        private bool enabled = false;
        private int cursorTop = 0;
        private int cursorLeft = 0;
        private int percentage = 0;
        private int count = 0;
        private int total = 0;
        private const int textWidth = 6;
    }
}