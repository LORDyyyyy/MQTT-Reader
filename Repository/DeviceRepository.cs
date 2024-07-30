using App.Data;
using App.Models;
using Microsoft.IdentityModel.Tokens;
using DeviceApp.Interfaces;
using App.Interfaces;

namespace App.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DataContext _context;

        public DeviceRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Device> GetDevices()
        {
            return _context.devices.OrderBy(d => d.Id).ToList();
        }


        Device IDeviceRepository.GetDevice(int Id)
        {
            return _context.devices.FirstOrDefault(d => d.Id == Id);
        }


        void IDeviceRepository.AddDevice(Device device)
        {
            _context.devices.Add(device);
            _context.SaveChanges();
        }

        void IDeviceRepository.UpdateDevice(Device device)
        {
            _context.devices.Update(device);
            _context.SaveChanges();
        }

        void IDeviceRepository.DeleteDevice(int Id)
        {
            var device = _context.devices.FirstOrDefault(d => d.Id == Id);
            if(device != null)
            {
                _context.devices.Remove(device);
                _context.SaveChanges();
            }
        }

        public ICollection<Branch> GetBranchByDevice(int Id)
        {
            return _context.branches.Where(d => d.Id == Id).ToList();
        }
    }
}
