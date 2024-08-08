using System.ComponentModel.DataAnnotations;

namespace App.DTO.BranchDTO
{
    public class CreateBranchDTO
    {
        [Required]
        public string Address { get; set; } = String.Empty;
        [Required]
        public string PhoneNumber { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string PostalCode { get; set; } = String.Empty;
    }
}
