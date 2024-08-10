using App.Models;

namespace App.Interfaces
{
    public interface IDeviceRepository
    {
        ICollection<Device> GetDevices();
        Device GetDevice(int Id);

        ICollection<Branch> GetBranchByDevice(int Id);

        void DeleteDevice(int Id);

        void AddDevice(Device device);

        void UpdateDevice(Device device);
    }
}











