namespace Parkopolis.Interfaces
{
    public interface IUI
    {
        void WriteLine(string message);
        void Write(string message);
        void Clear();
        string ReadLine();
    }
}
