namespace WebApi.Models;

public class TodoItem
{
    public int Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public string? Description { get; set; }

    public PriorityLevel Priority { get; set; } = PriorityLevel.Normal;

    public bool Completed { get; set; } = false;
}

public enum PriorityLevel
{
    Low, Normal, High
}