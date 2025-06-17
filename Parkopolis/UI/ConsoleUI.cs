using Parkopolis.Interfaces;

namespace Parkopolis.UI
{
    public class ConsoleUI : IUI
    {
        public void WriteLine(string message) => Console.WriteLine(message);
        public void Write(string message) => Console.Write(message);
        public void Clear() => Console.Clear();
        public string ReadLine() => Console.ReadLine() ?? string.Empty;
    }
}
