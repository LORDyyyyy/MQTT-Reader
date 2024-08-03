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

            // this.ReadCoils();
            // this.ReadDigitalInputs();
            this.ReadHoldingRegisters();
            // this.ReadInputRegisters()

            this.Disconnect();
        }

        public void ReadDigitalInputs(bool save = false)
        {
            throw new NotImplementedException();
        }

        public void ReadCoils(bool save = false)
        {
            throw new NotImplementedException();
        }

        public void ReadHoldingRegisters(bool save = false)
        {
            int[] holdingRegisters =
                this.modbusClient.ReadHoldingRegisters(this.startingAddress, this.quantity);

            Console.WriteLine("Read Holding Registers:");
            for (int i = 0; i < holdingRegisters.Length; i++)
                Console.WriteLine($"Register {this.startingAddress + i}: {holdingRegisters[i]}");

            if (!save)
                return;
        }

        public void ReadInputRegisters(bool save = false)
        {
            throw new NotImplementedException();
        }
    }
}