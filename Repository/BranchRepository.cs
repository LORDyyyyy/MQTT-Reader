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

        public ICollection<Branch> GetBranchs()
        {
            return _context.branches.OrderBy(p => p.Id).ToList();
        }
    }
}
