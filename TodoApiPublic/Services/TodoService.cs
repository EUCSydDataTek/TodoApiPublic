using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services;
public class TodoService : ITodoService
{
    private readonly TodoContext _context;

    public TodoService(TodoContext context)
    {
        _context = context;
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        return await _context.TodoItems.ToListAsync();
    }

    public async Task<List<TodoItem>> GetAsync()
    {
        return await _context.TodoItems.Where(t => t.Completed == false).ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task<TodoItem> CreateAsync(TodoItem todoItem)
    {
        todoItem.CreatedTime = DateTime.Now.ToLocalTime();
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }

    public async Task Update(int id, TodoItem todoItem)
    {
        _context.Entry(todoItem).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        _context.TodoItems.Remove(todoItem!);
        await _context.SaveChangesAsync();
    }
}
