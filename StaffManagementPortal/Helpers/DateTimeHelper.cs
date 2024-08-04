namespace StaffManagement.Portal.Helpers
{
    public static class DateTimeHelper
    {
        public static string DisplayDate(DateTime? date)
        {
            return date == null ? null : date.Value.ToString("yyyy-MM-dd");
        }
        public static string DisplayDateTime(DateTime? date)
        {
            return date!.Value.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
