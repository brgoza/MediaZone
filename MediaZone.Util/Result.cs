namespace MediaZone.Util;

public class Result
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public Result(bool success, string? message)
    {
        Success = success;
        Message = message;
    }
}

public class Result<T> : Result
{
    public T? Data { get; set; }
    public Result(bool success, string? message, T? data) : base(success, message)
    {
        Data = data;
    }
}