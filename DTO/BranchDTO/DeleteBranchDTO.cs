using System.ComponentModel.DataAnnotations;

namespace App.DTO.BranchDTO
{
    public class DeleteBranchDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
