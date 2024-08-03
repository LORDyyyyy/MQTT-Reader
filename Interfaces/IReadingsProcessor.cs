namespace App.Interfaces
{
    public interface IReadingsProcessor
    {
        void ProcessData(string ip, int port);
        bool Connect(string ip, int port);
        void Disconnect();
        void ReadDigitalInputs(bool save = false);
        void ReadCoils(bool save = false);
        void ReadHoldingRegisters(bool save = false);
        void ReadInputRegisters(bool save = false);
    }
}