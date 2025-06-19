namespace Parkopolis.Interfaces
{
    public interface IUI
    {
        void WriteLine(string message);
        void WriteLineColored(string message, string color);
        void Write(string message);
        void Clear();
        string ReadLine();
    }
}
