using App.Models;

namespace DeviceApp.Interfaces
{
    public interface IDeviceRepository
    {
        ICollection<Device> GetDevices();

    }
}
