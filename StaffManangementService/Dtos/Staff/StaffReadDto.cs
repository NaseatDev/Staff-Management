namespace StaffManagement.Service.Dtos.Staff
{
    public class StaffReadDto
    {
        public string StaffId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public int Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
