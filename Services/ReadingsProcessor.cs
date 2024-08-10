using EasyModbus;
using App.Models;
using App.Interfaces;
using App.Repository;

namespace App.Services
{
    public class ReadingsProcessor : IReadingsProcessor
    {
        public int startingAddress = 0;
        public int quantity = 10;
        private ModbusClient modbusClient;
        private readonly IDeviceRepository deviceRepository;

        public ReadingsProcessor(IDeviceRepository deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        public bool Connect(string ip, int port)
        {
            try
            {
                this.modbusClient = new ModbusClient(ip, port);
                this.modbusClient.Connect();

                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();

                return false;
            }
        }

        public void Disconnect()
        {
            modbusClient.Disconnect();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Disconnected from Modbus slave.");
            Console.ResetColor();
        }

        public async void ProcessData()
        {
            var devices = this.deviceRepository.GetDevices();

            foreach (var device in devices)
            {
                this.FireReadingTasks(device);
            }
        }

        public async void FireReadingTasks(Device device)
        {
            Console.WriteLine($"Trying to connect to device {device.Id} at {device.Ip}:{device.Port}");
            if (!this.Connect(device.Ip, int.Parse(device.Port)))
                return;

            int[] holdingRegisters = this.Read<int>(this.modbusClient.ReadHoldingRegisters);

            Console.WriteLine($"Read Holding Registers for device {device.Id}: ");
            for (int i = 0; i < holdingRegisters.Length; i++)
                Console.Write($"{holdingRegisters[i]}" + (i == this.quantity - 1 ? "\n" : " - "));

            this.Disconnect();
        }

        public T[] Read<T>(Func<int, int, T[]> func)
        {
            T[] readings =
                func(this.startingAddress, this.quantity);

            return readings;
        }
    }
}