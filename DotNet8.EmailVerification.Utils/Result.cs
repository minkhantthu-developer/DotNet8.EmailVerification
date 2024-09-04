namespace DotNet8.EmailVerification.Utils;

public class Result<T>
{
    public T Data { get; set; }
    public EnumStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }

    public static Result<T> Success(
        string message = "Success",
        EnumStatusCode statusCode = EnumStatusCode.Accepted)
    {
        return new Result<T>
        {
            Message = message,
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    public static Result<T> Success(
        T Data,
        string message = "Success",
        EnumStatusCode statusCode = EnumStatusCode.Accepted)
    {
        return new Result<T>
        {
            Data = Data,
            Message = message,
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    public static  Result<T> SaveSuccess(
        string message="Saving Successful.",
        EnumStatusCode statusCode=EnumStatusCode.Success)
    {
        return Result<T>.Success(message, statusCode);
    }

    public static Result<T> UpdateSuccess(
        string message="Updating Successful.",
        EnumStatusCode statusCode = EnumStatusCode.Success)
    {
        return Result<T>.Success(message,statusCode);
    }

    public static Result<T> Failure(
        string message="Fail",
        EnumStatusCode statusCode=EnumStatusCode.BadRequest)
    {
        return new Result<T>
        {
            Message = message,
            StatusCode = statusCode,
            IsSuccess = false
        };
    }

    public static Result<T> Failure(Exception ex)
    {
        return Result<T>.Failure(ex.ToString(),EnumStatusCode.InternalServerError);
    }

    public static Result<T> NotFound(string message="No data found")
    {
        return Result<T>.Failure(message, EnumStatusCode.NotFound);
    }

    public static Result<T> Duplicate(string message="Duplicate Data.")
    {
        return Result<T>.Failure(message, EnumStatusCode.Conflict);
    }
}
