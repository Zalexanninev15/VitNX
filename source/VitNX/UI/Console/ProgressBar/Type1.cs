using System;
using System.Text;
using System.Threading;

namespace VitNX.UI.Console.ProgressBar
{
    /// <summary>
    /// Console ProgressBar type 1.
    /// </summary>
    public class Type1 : IDisposable, IProgress<double>
    {
        private const int blockCount = 10;
        private readonly TimeSpan animationInterval = TimeSpan.FromSeconds(1.0 / 8);
        private const string animation = @"|/-\";
        private readonly Timer timer;
        private bool workdo = true;
        private string work;
        private double currentProgress = 0;
        private string currentText = string.Empty;
        private bool disposed = false;
        private int animationIndex = 0;
        private ConsoleColor pb = new ConsoleColor();

        public Type1()
        {
            timer = new Timer(TimerHandler);
            if (!System.Console.IsOutputRedirected)
                ResetTimer();
        }

        public void Report(double value)
        {
            value = Math.Max(0, Math.Min(1, value));
            Interlocked.Exchange(ref currentProgress, value);
        }

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="consoleColor">The console color.</param>
        public void SetColor(ConsoleColor consoleColor)
        {
            pb = consoleColor;
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="work1">The text.</param>
        public void SetText(string work1)
        {
            work = work1;
        }

        /// <summary>
        /// Stop this task or not use console progressbar.
        /// </summary>
        /// <param name="stopper">The bool value.</param>
        public void NotUsed(bool stopper)
        {
            workdo = stopper;
        }

        private void ResetTimer()
        {
            timer.Change(animationInterval, TimeSpan.FromMilliseconds(-1));
        }

        private void TimerHandler(object state)
        {
            lock (timer)
            {
                if (workdo == true)
                {
                    if (disposed)
                        return;
                    int progressBlockCount = (int)(currentProgress * blockCount);
                    int percent = (int)(currentProgress * 100);
                    string text = string.Format(work + ": [{0}{1}] {2,3}% {3}",
                        new string('=', progressBlockCount),
                        new string(' ', blockCount - progressBlockCount),
                        percent, animation[animationIndex++ % animation.Length]);
                    UpdateText(text);
                    ResetTimer();
                }
            }
        }

        private void UpdateText(string text)
        {
            int commonPrefixLength = 0;
            int commonLength = Math.Min(currentText.Length, text.Length);
            while (commonPrefixLength < commonLength &&
                text[commonPrefixLength] == currentText[commonPrefixLength])
                commonPrefixLength++;
            StringBuilder outputBuilder = new StringBuilder();
            outputBuilder.Append('\b', currentText.Length - commonPrefixLength);
            outputBuilder.Append(text.Substring(commonPrefixLength));
            int overlapCount = currentText.Length - text.Length;
            if (overlapCount > 0)
            {
                outputBuilder.Append(' ', overlapCount);
                outputBuilder.Append('\b', overlapCount);
            }
            System.Console.ForegroundColor = pb;
            System.Console.Write(outputBuilder);
            currentText = text;
        }

        public void Dispose()
        {
            lock (timer)
            {
                disposed = true;
                UpdateText(string.Empty);
            }
        }
    }
}