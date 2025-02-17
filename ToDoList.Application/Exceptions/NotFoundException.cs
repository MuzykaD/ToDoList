namespace ToDoList.Application.Exceptions;

internal class NotFoundException<TId> : Exception
{
    public TId ProvidedId { get; }

    public NotFoundException(string message, TId id) : base(message)
    {
        ProvidedId = id;
    }

    public NotFoundException(TId providedId)
    {
        ProvidedId = providedId;
    }
}
