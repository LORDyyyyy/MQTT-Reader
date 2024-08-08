 using App.Models;

namespace App.Interfaces
{
    public interface iActualReadingRepository
    {
        ICollection<ActualReadings> GetActualReadings();
        ActualReadings GetActualReading(int Id);
        ICollection<ActualReadings> GetDivActualReadings(int DivId);
    }
}
