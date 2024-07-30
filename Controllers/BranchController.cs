using App.Interfaces;
using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class BranchController : Controller
    {
        private readonly IBranchRepository _branchRepository;
        public BranchController(IBranchRepository branchRepository)
        {
            this._branchRepository = branchRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Branch>))]
        public IActionResult GetBranches() {
            var branches = _branchRepository.GetBranches();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(branches);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Branch))]
        [ProducesResponseType(404)]
        public IActionResult GetBranch(int id)
        {
            var branch = _branchRepository.GetBranch(id);
            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }

        [HttpGet("{id:int}/devices")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Device>))]
        [ProducesResponseType(404)]
        public IActionResult GetDevicesBtBranch(int id)
        {
            var devices = _branchRepository.GetDevicesByBranch(id);

            if (devices == null)
            {
                return NotFound();
            }

            return Ok(devices);
        }
        /*
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Branch))]
        [ProducesResponseType(400)]
        public IActionResult AddBranch([FromBody] Branch branch)
        {
            if (branch == null)
            {
                return BadRequest(ModelState);
            }

            _branchRepository.AddBranch(branch);

            return CreatedAtAction("GetBranch", new { id = branch.Id }, branch);
        }

        [HttpPut]
        [P]

        */
    }

}
