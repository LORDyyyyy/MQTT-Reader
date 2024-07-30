using App.Interfaces;
using App.Models;
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
            var branches = _branchRepository.GetBranchs();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(branches);
        }
    }
}
