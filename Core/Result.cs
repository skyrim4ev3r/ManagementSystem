namespace Project.Core
{
    public class Result<T>
    {
        public T Value { get; set; }
        public string ResultMessage { get; set; }
        public int ResultCode { get; set; }
        public static Result<T> Success(int resCode, string message, T value)
            => new Result<T>
            { Value = value, ResultMessage = message, ResultCode = resCode };
        public static Result<T> Success(int resCode, string message)
            => new Result<T> { ResultMessage = message, ResultCode = resCode };
        public static Result<T> Failure(int resCode, string message)
            => new Result<T> { ResultMessage = message, ResultCode = resCode };
    }
}
