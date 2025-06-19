using Parkopolis.Interfaces;

namespace Parkopolis.UI
{
    public class ConsoleUI : IUI
    {
        public void WriteLine(string message) => Console.WriteLine(message);
        public void Write(string message) => Console.Write(message);
        public void Clear() => Console.Clear();
        public string ReadLine() => Console.ReadLine() ?? string.Empty;

        public void WriteLineColored(string message, string color)
        {
            var originalColor = Console.ForegroundColor;
            ConsoleColor targetColor;

            if (Enum.TryParse<ConsoleColor>(color, true, out ConsoleColor parsedColor))
            {
                targetColor = parsedColor;
            }
            else
            {
                targetColor = originalColor;

                System.Diagnostics.Debug.WriteLine($"[DEV WARNING] Invalid color '{color}'. Using default color." +
                    $"Only vaild ConsoleColor will have effect.");
            }

            Console.ForegroundColor = targetColor;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }
    }
}
