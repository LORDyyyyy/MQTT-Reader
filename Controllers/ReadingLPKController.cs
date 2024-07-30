using App.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.Controllers
{
    [Route("api/readinglkp/[controller]")]
    [ApiController]
    public class ReadingLPKController : Controller
    {
        private readonly IReadingLKPRepository _readingLKPRepository;
        public ReadingLPKController(IReadingLKPRepository readingLKPRepository)
        {
            this._readingLKPRepository = readingLKPRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReadingLKP>))]
        public IActionResult GetReadingLKP()
        {
            var readinglkp = _readingLKPRepository.GetReadingLKPList();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(readinglkp);
        }
    }
}
