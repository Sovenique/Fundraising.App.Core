namespace Fundraising.App.Core.Models
{
    public class Result<T>
    {
        public Error Error { get; set; }

        public T Data { get; set; }

        public Result()
        {
        }



        public Result(ErrorCode errorCode, string errorText)
        {
            Error = new Error
            {
                ErrorCode = errorCode,
                Message = errorText
            };
        }
    }

    public class Error
    {
        public ErrorCode ErrorCode { get; set; }

        public string Message { get; set; }
    }

    public enum ErrorCode
    {
        Unspecified = 0,
        NotFound = 1,
        BadRequest = 2,
        Conflict = 3,
        InternalServerError = 4
    }
}
