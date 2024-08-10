using App.Models;
using EasyModbus;

namespace App.Interfaces
{
    public interface IReadingsProcessor
    {
        void ProcessData();

        bool Connect(ModbusClient modbusClient);
        void Disconnect(ModbusClient modbusClient);

        void FireReadingTasks(Device device);

        T[] Read<T>(Func<int, int, T[]> func);
    }
}