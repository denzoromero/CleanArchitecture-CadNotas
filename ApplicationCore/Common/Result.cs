namespace ApplicationCore.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? Message { get; }
        public Error? Error { get; }

        protected Result(bool isSuccess, string? message, Error? error)
        {
            if (isSuccess && error is not null)
                throw new InvalidOperationException("Successful result cannot contain an error.");

            if (!isSuccess && error is null)
                throw new InvalidOperationException("Failure result must contain an error.");

            IsSuccess = isSuccess;
            Message = message;
            Error = error;
        }

        public static Result Success(string? message = null) => new (true, message, null);
        public static Result Failure(Error error) => new (false, null, error);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(bool isSuccess, T? value, string? message, Error? error) : base(isSuccess, message, error)
        {
            Value = value;
        }


        public static Result<T> Success(T value,string? message = null) => new(true, value, message, null);
        public static new Result<T> Failure(Error error) => new(false, default, null, error);
    }

    public record Error(string Code, string Message);

}
