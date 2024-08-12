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
        private readonly iActualReadingRepository iActualReadingRepository;
        private readonly IReadingLKPRepository readingLKPRepository;

        public ReadingsProcessor(IDeviceRepository deviceRepository, iActualReadingRepository 
          iActualReadingRepository, IReadingLKPRepository readingLKPRepository)
        {
            this.deviceRepository = deviceRepository;
            this.iActualReadingRepository = iActualReadingRepository;
            this.readingLKPRepository = readingLKPRepository;
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
            var n = DateTime.Now;
            //n.AddMilliseconds(-n.Millisecond);
            n = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second);

            Console.WriteLine($"Trying to connect to Device {device.Id} at {device.Ip}:{device.Port}");

            var modbusClient = new ModbusClient(device.Ip, int.Parse(device.Port));
            if (!Connect(modbusClient))
                return;

            Console.WriteLine(modbusClient.GetHashCode());
            int[] holdingRegisters = Read<int>(modbusClient.ReadHoldingRegisters);

            Console.WriteLine($"Holding Registers for Device {device.Id}: ");
            for (int i = 0; i < holdingRegisters.Length; i++)
                Console.Write($"{holdingRegisters[i]}" + (i == quantity - 1 ? "\n" : " - "));
            /*
            var powerReading =
                holdingRegisters[this.readingLKPRepository.GetReadingLKPList().Where(x => x.Name.CompareTo("Power") == 0).FirstOrDefault().index];
            
            var currentReading =
                holdingRegisters[this.readingLKPRepository.GetReadingLKPList().Where(x => x.Name.CompareTo("Current") == 0).FirstOrDefault().index];
 
            var voltageReading =
                holdingRegisters[this.readingLKPRepository.GetReadingLKPList().Where(x => x.Name.CompareTo("Voltage") == 0).FirstOrDefault().index];
            */
            int powerReading = holdingRegisters[readingLKPRepository.GetIndexByName("Power").index];
            int currentReading = holdingRegisters[readingLKPRepository.GetIndexByName("Current").index];
            int voltageReading = holdingRegisters[readingLKPRepository.GetIndexByName("Voltage").index];

            var actualPower = new ActualReadings()
            {
                device = device,
                ReadingValue = powerReading,
                ReadingLKPId = readingLKPRepository.GetIndexByName("Power").Id,
                TimeStamp = n 
               };

            var actualCurrent = new ActualReadings()
            {
                device = device,
                ReadingValue = currentReading,
                ReadingLKPId = readingLKPRepository.GetIndexByName("Current").Id,
                TimeStamp = n 
            };

            var actualVoltage = new ActualReadings()
            {
                device = device,
                ReadingValue = voltageReading,
                ReadingLKPId = readingLKPRepository.GetIndexByName("Voltage").Id,
                TimeStamp = n

            };

            this.iActualReadingRepository.AddActualReading(actualVoltage);
            this.iActualReadingRepository.AddActualReading(actualCurrent);
            this.iActualReadingRepository.AddActualReading(actualPower); 

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