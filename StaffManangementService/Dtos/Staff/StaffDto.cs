using System.ComponentModel.DataAnnotations;

namespace StaffManagement.Service.Dtos.Staff
{
    public class StaffDto
    {
        [Required]
        [StringLength(8)]
        public string StaffId { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        [Range(1, 2)]
        public int Gender { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
