using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{

    public class Branch
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Address { get; set; } = String.Empty;
        [Required]
        public string PhoneNumber { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string PostalCode { get; set; } = String.Empty;
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
