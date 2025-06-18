using Parkopolis.Interfaces;

namespace Parkopolis.UI
{
    public class ConsoleUI : IUI
    {
        public void WriteLine(string message) => Console.WriteLine(message);
        public void Write(string message) => Console.Write(message);
        public void Clear() => Console.Clear();
        public string ReadLine() => Console.ReadLine() ?? string.Empty;

        public void WriteLineColored<T>(string message, T color)
        {
            // Enforces ConsoleColor for the console UI at runtime
            if (color is not ConsoleColor consoleColor)
            {
                throw new ArgumentException($"ConsoleUI only supports ConsoleColor, but received {typeof(T).Name}");
            }

            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }
    }
}
