using System.Text;

namespace VitNX.UI.Console.ProgressBar
{
    /// <summary>
    /// Console ProgressBar type 2.
    /// </summary>
    public static class Type2
    {
        private const char progressChar = '-';
        private const int progressBarSpacing = 11;
        private const int maxProgressChars = 10;

        /// <summary>
        /// Prints the progress in console.
        /// </summary>
        /// <param name="progressPercentage">The progress percentage.</param>
        /// <param name="textWithEvent">Add text in console with event.</param>
        public static void PrintProgressToConsole(double progressPercentage, string textWithEvent)
        {
            int progressCharsToPrint = (int)progressPercentage / maxProgressChars;
            StringBuilder sb = new StringBuilder();
            sb.Append($"{textWithEvent}: ");
            AddProgressBar(ref sb, progressCharsToPrint);
            AddSpacingToString(ref sb, progressCharsToPrint);
            AddProgressValue(ref sb, progressPercentage);
            string progressString = sb.ToString();
            PrintProgressStringToConsole(progressString);
        }

        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = System.Console.CursorTop;
            System.Console.SetCursorPosition(0, System.Console.CursorTop);
            System.Console.Write(new string(' ', System.Console.WindowWidth));
            System.Console.SetCursorPosition(0, currentLineCursor);
        }

        private static void PrintProgressStringToConsole(string progressString)
        {
            ClearCurrentConsoleLine();
            System.Console.Write(progressString);
        }

        private static void AddProgressValue(ref StringBuilder sb, double progressPercentage)
        {
            sb.Append(progressPercentage + "%" + " Complete");
        }

        private static void AddSpacingToString(ref StringBuilder sb, int progressCharsToPrint)
        {
            int spacesToAdd = progressBarSpacing - progressCharsToPrint;
            for (int i = 0; i < spacesToAdd; i++)
                sb.Append(" ");
        }

        private static void AddProgressBar(ref StringBuilder sb, int progressCharsToPrint)
        {
            for (int i = 0; i < progressCharsToPrint; i++)
                sb.Append(progressChar);
        }
    }
}