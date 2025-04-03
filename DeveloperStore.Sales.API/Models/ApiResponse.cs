namespace DeveloperStore.Sales.API.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ApiResponse(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }

        public static ApiResponse Ok(string? message = null) => new(true, message);
        public static ApiResponse Fail(string message) => new(false, message);
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }

        public ApiResponse(bool success, string? message = null, T? data = default)
            : base(success, message)
        {
            Data = data;
        }

        public static ApiResponse<T> Ok(T data, string? message = null)
            => new(true, message, data);

        public static new ApiResponse<T> Fail(string message)
            => new(false, message, default);
    }
}
