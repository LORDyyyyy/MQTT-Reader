using AutoMapper;
using App.Data;
using App.Models;
using App.DTO;
using App.DTO.DeviceDTO;

namespace App.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Device, DeviceDTO>();

            CreateMap<DeviceDTO, Device>();
        }
    }
}
