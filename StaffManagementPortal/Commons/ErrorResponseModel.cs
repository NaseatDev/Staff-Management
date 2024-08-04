namespace StaffManagement.Portal.Commons
{
    public class ErrorResponseModel
    {
        public string? Message { get; set; }
        public bool Succeeded { get; set; }
        public Exception? Exception { get; set; }
        public int StatusCode { get; set; }
    }
}
