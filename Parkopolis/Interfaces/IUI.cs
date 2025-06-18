namespace Parkopolis.Interfaces
{
    public interface IUI
    {
        void WriteLine(string message);
        void WriteLineColored<T>(string message, T color); // Generic, to open up for other forms of UI's
        void Write(string message);
        void Clear();
        string ReadLine();
    }
}
