using App.Data;
using App.Interfaces;
using App.Models;

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

        Device IBranchRepository.GetDeviceByBranch(int id)
        {
            var branch = 
            return _context.branches
        }

        void IBranchRepository.UpdateBranch(Branch branch)
        {
            throw new NotImplementedException();
        }


        void IBranchRepository.AddBranch(Branch branch)
        {
            throw new NotImplementedException();
        }

        void IBranchRepository.DeleteBranch(int id)
        {
            throw new NotImplementedException();
        }

    }
}
