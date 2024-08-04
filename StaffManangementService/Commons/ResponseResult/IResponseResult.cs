namespace StaffManagement.Service.Commons.ResponseResult
{
    public interface IResponseResult<T>
    {
        string? Messages { get; set; }
        bool Succeeded { get; set; }
        T? Data { get; set; }
        Exception Exception { get; set; }
        int StatusCode { get; set; }
    }
}
