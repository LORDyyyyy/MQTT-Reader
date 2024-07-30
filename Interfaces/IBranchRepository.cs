using App.Models;

namespace App.Interfaces
{
    public interface IBranchRepository
    {
        ICollection<Branch> GetBranchs();
    }
}
