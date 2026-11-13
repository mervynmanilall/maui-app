using MauiTodo.Models;

namespace MauiTodo.Services;

public class InMemoryTodoService : ITodoService
{
    readonly List<TodoItem> items =
    [
        new TodoItem { Id = 1, Title = "Submit .NET Conf slides" },
        new TodoItem { Id = 2, Title = "Charge the demo phone", IsDone = true },
        new TodoItem { Id = 3, Title = "Buy dholl puri for the team" },
    ];

    int nextId = 4;

    public Task<List<TodoItem>> GetAllAsync() => Task.FromResult(items.ToList());

    public Task AddAsync(TodoItem item)
    {
        item.Id = nextId++;
        items.Add(item);
        return Task.CompletedTask;
    }

    // Items are shared objects, so there's nothing to save. A database would save here.
    public Task UpdateAsync(TodoItem item) => Task.CompletedTask;

    public Task DeleteAsync(TodoItem item)
    {
        items.Remove(item);
        return Task.CompletedTask;
    }
}