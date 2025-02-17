namespace ToDoList.Domain.Entities;

public class ToDoList
{
    public ToDoList(string name, string userId)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        UserId = userId;
    }

    public ToDoList(string id, string name, string userId)
    {
        Id = id;
        Name = name;
        UserId = userId;
    }

    public string Id { get; private set; }

    public string Name { get; set; }

    public string UserId { get; set; }

    public HashSet<string> SharedTo { get; private set; } = [];

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public bool CanBeModifiedBy(string userId) => UserId.Equals(userId);
}
