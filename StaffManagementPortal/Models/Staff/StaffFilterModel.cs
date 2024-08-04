namespace StaffManagement.Portal.Models.Staff
{
    public class StaffFilterModel
    {
        public string SearchText { get; set; }
        public string? StaffId { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Gender { get; set; }
    }
}
