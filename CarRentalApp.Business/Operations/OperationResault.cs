namespace CarRentalApp.Business.Models
{
    public class OperationResult<T>
    {
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T> { IsSuccessful = true, Data = data };
        }

        public static OperationResult<T> Failure(string errorMessage)
        {
            return new OperationResult<T> { IsSuccessful = false, ErrorMessage = errorMessage };
        }
    }
}
