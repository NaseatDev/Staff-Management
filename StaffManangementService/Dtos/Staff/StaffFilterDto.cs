namespace StaffManagement.Service.Dtos.Staff
{
    public class StaffFilterDto
    {
        public string? StaffId { get; set; }

        public string? SearchText { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Gender { get; set; }
    }
}
