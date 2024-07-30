
using App.Data;
using App.Models;
using DeviceApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly DataContext context;

        public DeviceController(IDeviceRepository deviceRepository, DataContext context)
        {
            _deviceRepository = deviceRepository;
            this.context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Device>))]

        public IActionResult GetDevices()
        {
            var device = _deviceRepository.GetDevices();

            if(!ModelState.IsValid) 
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
        public IActionResult AddDevice([FromBody] Device device)
        {
            if (device == null)
            {
                return BadRequest(ModelState);
            }

            _deviceRepository.AddDevice(device);

            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }

        [HttpPut]
        [ProducesResponseType(201, Type = typeof(Device))]
        [ProducesResponseType(400)]
        public IActionResult UpdateDevice([FromBody] Device device)
        {
            if (device == null)
            {
                return BadRequest(ModelState);
            }

            _deviceRepository.AddDevice(device);

            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }



    }
}
