using WebApi.Models;

namespace WebApi.Services;

public interface ITodoService
{
    Task<List<TodoItem>> GetAsync();
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<TodoItem> CreateAsync(TodoItem todoItem);
    Task Update(int id, TodoItem todoItem);
    Task Delete(int id); 
}
