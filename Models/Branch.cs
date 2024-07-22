using System;
namespace App.Models
{

    public class Branch
    {
        public int Id { get; set; }
        public string String { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PostalCode { get; set; } = String.Empty;
        public virtual ICollection<Device> Devices { get; set; }
    }
}
