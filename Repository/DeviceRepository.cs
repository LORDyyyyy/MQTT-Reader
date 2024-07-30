using App.Data;
using App.Models;
using Microsoft.IdentityModel.Tokens;

namespace App.Repository
{
    public class DeviceRepository
    {
        private readonly DataContext _context;
        public DeviceRepository(DataContext context)
        {
            this._context = context;
        }

        public ICollection<Device> GetDevices() 
        {
            return _context.devices.OrderBy(d => d.Id).ToList();
        }
    }
}
