using App.Models;

namespace App.Interfaces
{
    public interface IReadingsProcessor
    {
        void ProcessData();

        bool Connect(string ip, int port);
        void Disconnect();

        void FireReadingTasks(Device device);

        T[] Read<T>(Func<int, int, T[]> func);
    }
}