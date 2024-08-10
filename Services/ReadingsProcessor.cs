using EasyModbus;
using App.Models;
using App.Interfaces;

namespace App.Services
{
    public class ReadingsProcessor : IReadingsProcessor
    {
        public int startingAddress = 0;
        public int quantity = 10;

        private readonly IDeviceRepository deviceRepository;

        public ReadingsProcessor(IDeviceRepository deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        public bool Connect(ModbusClient modbusClient)
        {
            try
            {
                modbusClient.Connect();

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

        public void Disconnect(ModbusClient modbusClient)
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

            Console.WriteLine($"Trying to connect to Device {device.Id} at {device.Ip}:{device.Port}");

            var modbusClient = new ModbusClient(device.Ip, int.Parse(device.Port));
            if (!Connect(modbusClient))
                return;

            Console.WriteLine(modbusClient.GetHashCode());
            int[] holdingRegisters = Read<int>(modbusClient.ReadHoldingRegisters);

            Console.WriteLine($"Holding Registers for Device {device.Id}: ");
            for (int i = 0; i < holdingRegisters.Length; i++)
                Console.Write($"{holdingRegisters[i]}" + (i == quantity - 1 ? "\n" : " - "));

            Disconnect(modbusClient);
        }

        public T[] Read<T>(Func<int, int, T[]> func)
        {
            try
            {
                T[] readings =
                    func(startingAddress, quantity);

                return readings;
            }
            catch (Exception ex)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
                return new T[] { };
            }
        }
    }
}