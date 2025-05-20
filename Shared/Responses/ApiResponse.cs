
namespace Shared.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public bool Show { get; set; } = true;
        public List<string>? Errors { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "", int statusCode = 200, bool show = false)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = statusCode,
                Show = show
            };
        }

        public static ApiResponse<T> FailResponse(string message, int statusCode = 400, bool show = true, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = statusCode,
                Show = show,
                Errors = errors
            };
        }

    }
}


