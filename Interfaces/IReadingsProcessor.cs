namespace App.Interfaces
{
    public interface IReadingsProcessor
    {
        void ProcessData(string ip, int port);
        bool Connect(string ip, int port);
        void Disconnect();

        T[] Read<T>(Func<int, int, T[]> func, bool save = false);
    }
}