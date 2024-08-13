namespace StaffManagement.Service.Dtos.AppSetting
{
    public class FixedWindowLimiterOptionsDto
    {
        public int WindowSeconds { get; set; }
        public int PermitLimit { get; set; }
        public string QueueProcessingOrder { get; set; } = string.Empty;
        public int QueueLimit { get; set; }
    }
}
