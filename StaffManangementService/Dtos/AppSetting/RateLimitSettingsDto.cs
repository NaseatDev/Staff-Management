namespace StaffManagement.Service.Dtos.AppSetting
{
    public class RateLimitSettingsDto
    {
        public FixedWindowLimiterOptionsDto FixedWindowLimiter { get; set; } = new();
    }
}
