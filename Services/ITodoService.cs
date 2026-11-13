using MauiTodo.Models;

namespace MauiTodo.Services;

// The view model only knows this interface, never where the data really lives.
public interface ITodoService
{
    Task<List<TodoItem>> GetAllAsync();
    Task AddAsync(TodoItem item);
    Task UpdateAsync(TodoItem item);
    Task DeleteAsync(TodoItem item);
}