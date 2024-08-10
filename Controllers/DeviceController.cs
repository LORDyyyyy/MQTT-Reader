
using App.DTO;
using App.DTO.DeviceDTO;
using App.Models;
using App.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace App.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper mapper;

        //        private readonly DataContext context;

        public DeviceController(IDeviceRepository deviceRepository, IMapper mapper)//, DataContext context)
        {
            this._deviceRepository = deviceRepository;
            this.mapper = mapper;
            //this.context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Device>))]

        public IActionResult GetDevices()
        {
            var device = _deviceRepository.GetDevices();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(device);

        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Device))]
        [ProducesResponseType(404)]
        public IActionResult GetDevice(int Id)
        {
            var device = _deviceRepository.GetDevice(Id);
            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        [HttpGet("{Id:int}/branch")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Device>))]
        [ProducesResponseType(404)]
        public IActionResult GetBranchByDevice(int Id)
        {
            var branchs = _deviceRepository.GetBranchByDevice(Id);

            if (branchs == null)
            {
                return NotFound();
            }

            return Ok(branchs);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Device))]
        [ProducesResponseType(400)]
        public IActionResult AddDevice([FromBody] DeviceDTO deviceDTO)
        {
            if (deviceDTO == null)
            {
                return BadRequest(ModelState);
            }

            var device = this.mapper.Map<Device>(deviceDTO);

            _deviceRepository.AddDevice(device);

            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(201, Type = typeof(Device))]
        [ProducesResponseType(400)]
        public IActionResult UpdateDevice([FromBody] DeviceDTO deviceDTO)
        {
            if (deviceDTO== null)
            {
                return BadRequest(ModelState);
            }

            var device = this.mapper.Map<Device>(deviceDTO);


            _deviceRepository.UpdateDevice(device);

            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDevice(int Id)
        {
            var device = _deviceRepository.GetDevice(Id);
            if (device == null)
            { return NotFound(); }

            _deviceRepository.DeleteDevice(Id);
            return Ok();
        }
    }
}
