using EasyModbus;
using App.Data;
using App.Interfaces;

namespace App.Services
{
    public class ReadingsProcessor : IReadingsProcessor
    {
        public int startingAddress = 0;
        public int quantity = 10;
        private ModbusClient modbusClient;
        private readonly DataContext context;

        public ReadingsProcessor(DataContext context)
        {
            this.context = context;
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
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public void Disconnect()
        {
            modbusClient.Disconnect();
            Console.WriteLine("Disconnected from Modbus slave.");
        }

        public void ProcessData(string ip, int port)
        {
            if (!this.Connect(ip, port))
                return;

            Console.WriteLine($"Connected at: {ip}:{port}");

            bool[] readDigitalInputs = this.Read<bool>(this.modbusClient.ReadDiscreteInputs);
            int[] holdingRegisters = this.Read<int>(this.modbusClient.ReadHoldingRegisters);
            int[] inputRegisters = this.Read<int>(this.modbusClient.ReadInputRegisters);
            bool[] readCoils = this.Read<bool>(this.modbusClient.ReadCoils);

            Console.WriteLine("Read Holding Registers:");
            for (int i = 0; i < holdingRegisters.Length; i++)
                Console.Write($"{holdingRegisters[i]}" + (i == this.quantity - 1 ? "\n" : " - "));

            Console.WriteLine("Read Input Registers:");
            for (int i = 0; i < inputRegisters.Length; i++)
                Console.Write($"{inputRegisters[i]}" + (i == this.quantity - 1 ? "\n" : " - "));

            Console.WriteLine("Read Coils:");
            for (int i = 0; i < inputRegisters.Length; i++)
                Console.Write($"{readCoils[i]}" + (i == this.quantity - 1 ? "\n" : " - "));

            Console.WriteLine("Read Digital Inputs:");
            for (int i = 0; i < inputRegisters.Length; i++)
                Console.Write($"{readDigitalInputs[i]}" + (i == this.quantity - 1 ? "\n" : " - "));

            this.Disconnect();
        }

        public T[] Read<T>(Func<int, int, T[]> func, bool save = false)
        {
            T[] readings =
                func(this.startingAddress, this.quantity);

            // save readings to database logic
            if (save) { };

            return readings;
        }
    }
}