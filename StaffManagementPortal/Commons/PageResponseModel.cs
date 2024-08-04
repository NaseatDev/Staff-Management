namespace StaffManagement.Portal.Commons
{
    public class PageResponseModel<T>
    {
        public string? Messages { get; set; }
        public bool Succeeded { get; set; }
        public T? Data { get; set; }
        public Exception? Exception { get; set; }
        public int StatusCode { get; set; }
    }
}
