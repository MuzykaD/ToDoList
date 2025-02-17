namespace ToDoList.Application.Exceptions;

public class NotFoundException : Exception
{
    public string ProvidedId { get; }

    public NotFoundException(string message, string id) : base(message)
    {
        ProvidedId = id;
    }

    public NotFoundException(string providedId)
    {
        ProvidedId = providedId;
    }
}
