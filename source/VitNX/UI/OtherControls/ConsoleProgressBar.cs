﻿using System;
using System.Text;
using System.Threading;

namespace VitNX.UI.OtherControls
{
    public class ConsoleProgressBar : IDisposable, IProgress<double>
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

        public ConsoleProgressBar()
        {
            timer = new Timer(TimerHandler);
            if (!Console.IsOutputRedirected)
                ResetTimer();
        }

        public void Report(double value)
        {
            value = Math.Max(0, Math.Min(1, value));
            Interlocked.Exchange(ref currentProgress, value);
        }

        public void SetColor(ConsoleColor my_pb)
        {
            pb = my_pb;
        }

        public void SetText(string work1)
        {
            work = work1;
        }

        public void NotUsed(bool pbd)
        {
            workdo = pbd;
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
                        new string('▰', progressBlockCount),
                        new string('-', blockCount - progressBlockCount),
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
            while (commonPrefixLength < commonLength && text[commonPrefixLength] == currentText[commonPrefixLength])
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
            Console.ForegroundColor = pb;
            Console.Write(outputBuilder);
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