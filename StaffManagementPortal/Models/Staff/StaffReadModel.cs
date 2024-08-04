namespace StaffManagement.Portal.Models.Staff
{
    public class StaffReadModel
    {
        public string StaffId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime? Birthday { get; set; }
        public int Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
