using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StaffManagement.Portal.Models.Staff
{
    public class StaffModel
    {
        [Required]
        [StringLength(8)]
        public string StaffId { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public DateTime? Birthday { get; set; }
        [Required]
        [Range(1, 2)]
        public int Gender { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? CreatedDate { get; set; }
    }
}
