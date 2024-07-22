using System;
using App.Models;
namespace App.Models {

    public class ActualReadings
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int ReadingLKPId { get; set; }
        public decimal ReadingValue { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public Device device { get; set; }
        public ReadingLKP readingLKP { get; set; }



    }
}