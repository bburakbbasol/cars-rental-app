namespace CarRentalApp.WebApi.Models.Common
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public static ApiResponse<T> Success(T data, string message = null)
        {
            return new ApiResponse<T>
            {
                Data = data,
                IsSuccessful = true,
                Message = message,
                Errors = null
            };
        }

        public static ApiResponse<T> Fail(string message, List<string> errors = null)
        {
            return new ApiResponse<T>
            {
                Data = default,
                IsSuccessful = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
