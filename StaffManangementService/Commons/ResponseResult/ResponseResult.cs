namespace StaffManagement.Service.Commons.ResponseResult
{
    public class ResponseResult<T> : IResponseResult<T>
    {
        public string? Messages { get; set; }
        public bool Succeeded { get; set; }
        public T? Data { get; set; }
        public Exception? Exception { get; set; }
        public int StatusCode { get; set; }

        #region Async Methods 

        #region Success Methods

        public static Task<ResponseResult<T>> SuccessAsync(string message, int statusCode = 200)
        {
            return Task.FromResult(new ResponseResult<T>
            {
                Succeeded = true,
                StatusCode = statusCode,
                Messages = message
            });
        }

        public static Task<ResponseResult<T>> SuccessAsync(T data, int statusCode = 200)
        {
            return Task.FromResult(new ResponseResult<T>
            {
                Succeeded = true,
                StatusCode = statusCode,
                Data = data
            });
        }

        public static Task<ResponseResult<T>> SuccessAsync(T data, string message, int statusCode = 200)
        {
            return Task.FromResult(new ResponseResult<T>
            {
                Succeeded = true,
                Messages = message,
                StatusCode = statusCode,
                Data = data
            });
        }

        #endregion

        #region Failure Methods 

        public static Task<ResponseResult<T>> FailureAsync(string message, int statusCode = 400)
        {
            return Task.FromResult(new ResponseResult<T>
            {
                StatusCode = statusCode,
                Messages = message
            });
        }


        public static Task<ResponseResult<T>> FailureAsync(T data, int statusCode = 400)
        {
            return Task.FromResult(new ResponseResult<T>
            {
                Data = data,
                StatusCode = statusCode,
            });
        }

        public static Task<ResponseResult<T>> FailureAsync(T data, string message, int statusCode = 400)
        {
            return Task.FromResult(new ResponseResult<T>
            {
                Data = data,
                Messages = message,
                StatusCode = statusCode
            });
        }


        public static Task<ResponseResult<T>> FailureAsync(Exception exception, int statusCode = 400)
        {
            return Task.FromResult(new ResponseResult<T>
            {
                Exception = exception,
                StatusCode = statusCode
            });
        }

        #endregion

        #endregion
    }
}
