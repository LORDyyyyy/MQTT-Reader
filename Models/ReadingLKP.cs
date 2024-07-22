using App.Models;
using System;

namespace App.Models
{
    public class ReadingLKP
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Unit { get; set; } = String.Empty;
        public virtual ICollection<ActualReadings> ActualReadings { get; set; }
    }
}