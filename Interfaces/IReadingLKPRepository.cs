using App.Models;

namespace App.Interfaces
{
    public interface IReadingLKPRepository
    {
        ICollection<ReadingLKP> GetReadingLKPList();
        ReadingLKP GetIndexByName(string name);
    }
}
