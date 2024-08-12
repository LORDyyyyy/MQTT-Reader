using App.Data;
using App.Interfaces;
using App.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Repository
{
    public class ActualReadingRepository: iActualReadingRepository
    {
        private readonly DataContext _context;
        //private readonly DataContext _context1;

        public ActualReadingRepository(DataContext context)
        {
            this._context = context;
        }
        public ActualReadings GetActualReading(int Id)
        {
            return _context.actualReadings.FirstOrDefault(b=>b.Id==Id);
        }
        public ICollection<ActualReadings> GetActualReadings()
        {
            return _context.actualReadings.OrderBy(p=>p.Id).ToList();

        }
        public ICollection<ActualReadings> GetDivActualReadings(int DivId)
        {
            return _context.actualReadings.Where(b=>b.DeviceId==DivId).ToList();
        }
        public void AddActualReading (ActualReadings ActualReadings)
        {
            _context.actualReadings.Add(ActualReadings);
            _context.SaveChanges();
        }
    }
}
