namespace MauiTodo.Models;

// Plain C# class representing a single to-do item.
public class TodoItem
{
    public string Title { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}