using App.Data;
using App.Interfaces;
using App.Models;

namespace App.Repository
{
    public class ReadingLKPRepository : IReadingLKPRepository
    {
        private readonly DataContext _context;
        public ReadingLKPRepository(DataContext context)
        {
            this._context = context;
        }
        public ICollection<ReadingLKP> GetReadingLKPList ()
        {
            return _context.readingLKPs.OrderBy(p => p.Id).ToList();
        }
        public ReadingLKP GetIndexByName (string name)
        {
            return _context.readingLKPs.Where(x => x.Name.CompareTo(name) == 0).FirstOrDefault();
        }
    }
}
