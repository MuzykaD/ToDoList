namespace ToDoList.Domain.Entities;

public class ToDoList
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string UserId { get; set; }

    public ICollection<string> SharedTo { get; set; } = [];

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
