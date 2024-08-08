using App.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using App.DTO.BranchDTO;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository _branchRepository;

        public BranchController(IBranchRepository branchRepository)
        {
            this._branchRepository = branchRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BranchDTO>))]
        public IActionResult GetBranches()
        {
            var branches = _branchRepository.GetBranches();
            var branchDTOs = branches.Select(b => new BranchDTO
            {
                Id = b.Id,
                Address = b.Address,
                PhoneNumber = b.PhoneNumber,
                Email = b.Email,
                PostalCode = b.PostalCode
            }).ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(branchDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BranchDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetBranch(int id)
        {
            var branch = _branchRepository.GetBranch(id);

            if (branch == null)
            {
                return NotFound();
            }

            var branchDTO = new BranchDTO
            {
                Id = branch.Id,
                Address = branch.Address,
                PhoneNumber = branch.PhoneNumber,
                Email = branch.Email,
                PostalCode = branch.PostalCode
            };

            return Ok(branchDTO);
        }

        [HttpGet("{id:int}/devices")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Device>))]
        [ProducesResponseType(404)]
        public IActionResult GetDevicesByBranch(int id)
        {
            var devices = _branchRepository.GetDevicesByBranch(id);

            if (devices == null)
            {
                return NotFound();
            }

            return Ok(devices);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BranchDTO))]
        [ProducesResponseType(400)]
        public IActionResult AddBranch([FromBody] CreateBranchDTO createBranchDTO)
        {
            if (createBranchDTO == null)
            {
                return BadRequest(ModelState);
            }

            var branch = new Branch
            {
                Address = createBranchDTO.Address,
                PhoneNumber = createBranchDTO.PhoneNumber,
                Email = createBranchDTO.Email,
                PostalCode = createBranchDTO.PostalCode
            };

            _branchRepository.AddBranch(branch);

            var branchDTO = new BranchDTO
            {
                Id = branch.Id,
                Address = branch.Address,
                PhoneNumber = branch.PhoneNumber,
                Email = branch.Email,
                PostalCode = branch.PostalCode
            };

            return CreatedAtAction("GetBranch", new { id = branch.Id }, branchDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBranch(int id, [FromBody] UpdateBranchDTO updateBranchDTO)
        {
            if (updateBranchDTO == null)
            {
                return BadRequest(ModelState);
            }

            var existingBranch = _branchRepository.GetBranch(id);
            if (existingBranch == null)
            {
                return NotFound();
            }

            existingBranch.Address = updateBranchDTO.Address;
            existingBranch.PhoneNumber = updateBranchDTO.PhoneNumber;
            existingBranch.Email = updateBranchDTO.Email;
            existingBranch.PostalCode = updateBranchDTO.PostalCode;

            _branchRepository.UpdateBranch(existingBranch);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBranch(int id)
        {
            var branch = _branchRepository.GetBranch(id);
            if (branch == null)
            {
                return NotFound();
            }

            _branchRepository.DeleteBranch(id);

            return NoContent();
        }
    }
}
