using System.Text.Json.Serialization;

namespace Kervan.SharedKernel.Application.Common.Wrappers;

public class ApiResponse<T>
{
    public bool IsSuccess { get; private set; }

    [JsonIgnore]
    public int StatusCode { get; private set; }

    public List<string>? Errors { get; private set; }

    public T? Data { get; private set; }

    protected ApiResponse(int statusCode)
    {
        StatusCode = statusCode;
        IsSuccess = true;
    }

    protected ApiResponse(T data, int statusCode)
    {
        StatusCode = statusCode;
        Data = data;
        IsSuccess = true;
    }

    protected ApiResponse(List<string> errors, int statusCode)
    {
        StatusCode = statusCode;
        Errors = errors;
        IsSuccess = false;
    }

    public static ApiResponse<T> Success(T data, int statusCode = 200)
    {
        return new ApiResponse<T>(data, statusCode);
    }

    public static ApiResponse<T> Success(int statusCode = 204)
    {
        return new ApiResponse<T>(statusCode);
    }

    public static ApiResponse<T> Fail(List<string> errors, int statusCode = 400)
    {
        return new ApiResponse<T>(errors, statusCode);
    }

    public static ApiResponse<T> Fail(string error, int statusCode = 400)
    {
        return new ApiResponse<T>(new List<string> { error }, statusCode);
    }
}