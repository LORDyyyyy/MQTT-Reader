using App.Models;

namespace App.Interfaces
{
    public interface IBranchRepository
    {
        ICollection<Branch> GetBranches();
        Branch GetBranch(int id);
        Device GetDeviceByBranch(int id);
        void DeleteBranch(int id);
        void AddBranch(Branch branch);
        void UpdateBranch(Branch branch);
    }
}
