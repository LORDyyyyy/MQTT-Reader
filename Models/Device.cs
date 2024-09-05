using App.Models;
using System;
using System.Security.Permissions;

namespace App.Models
{
    public class Device
    {
        public int Id { get; set; }

        public string Ip { get; set; } = string.Empty;

        public string Port { get; set; } = string.Empty;

        public DateTime? Last_Reading_Time { get; set; }

        public string type { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Model { get; set; } = string.Empty;

        public string? UserName { get; set; } = string.Empty;

        public string? PassWord { get; set; } = string.Empty;

        public DateTime Installation_Date { get; set; }

        public string Life_Time { get; set; } = string.Empty;

        public string Maunfacturer { get; set; } = string.Empty;

        public string Made_In_Counry { get; set; } = string.Empty;

        public int State { get; set; }

        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public ICollection<ActualReadings> ActualReading { get; set; } = new List<ActualReadings>();
    }
}

