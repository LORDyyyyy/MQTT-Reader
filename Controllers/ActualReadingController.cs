using App.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/readinglkp/[controller]")]
    [ApiController]
    public class ActualReadingController : Controller
    {
        private readonly iActualReadingRepository _actualReadingRepository;

        public ActualReadingController(iActualReadingRepository actualReadingRepository)
        {
            _actualReadingRepository = actualReadingRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ActualReadings>))]
        public IActionResult GetActualReadings()
        {
            var ActualReadings = _actualReadingRepository.GetActualReadings();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(ActualReadings);
        }
        public IActionResult GetActualReading(int Id)
        {
            var ActualReadings = _actualReadingRepository.GetActualReading(Id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(ActualReadings);
        }
        public IActionResult GetDivActualReadings(int DivId)
        {
            var ActualReadings = _actualReadingRepository.GetDivActualReadings(DivId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(ActualReadings);
        }
    }
}
