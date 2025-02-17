namespace ToDoList.Application.Exceptions;

public class BadRequestException : Exception
{
    public object? FaultObject { get; set; }

    public BadRequestException(string? message) : base(message)
    {
    }

    public BadRequestException(string? message, object? faultObject) : base(message)
    {
        FaultObject = faultObject;
    }
}
