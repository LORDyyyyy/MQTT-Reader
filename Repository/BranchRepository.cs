using App.Data;
using App.Interfaces;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly DataContext _context;

        public BranchRepository(DataContext context)
        {
            this._context = context;
        }

        public ICollection<Branch> GetBranches()
        {
            return _context.branches.OrderBy(p => p.Id).ToList();
        }
        Branch IBranchRepository.GetBranch(int id)
        {
            return _context.branches.FirstOrDefault(b => b.Id == id);
        }

        public ICollection<Device> GetDevicesByBranch(int id)
        {
            return _context.devices.Where(b => b.Branch_Id == id).ToList();
        }

        void IBranchRepository.UpdateBranch(Branch branch)
        {
            _context.branches.Update(branch);
            _context.SaveChanges();
        }


        void IBranchRepository.AddBranch(Branch branch)
        {
            _context.branches.Add(branch);
            _context.SaveChanges();
        }

        void IBranchRepository.DeleteBranch(int id)
        {
            var branch = _context.branches.FirstOrDefault(b => b.Id == id);
            if (branch != null)
            {
                _context.branches.Remove(branch);
                _context.SaveChanges();
            }
        }

    }
}
