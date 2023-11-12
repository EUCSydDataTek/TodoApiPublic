using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().HasData(
            new TodoItem { Id = 1, CreatedTime = DateTime.Now, Description = "Løbe en tur", Priority = PriorityLevel.High, Completed = false },
            new TodoItem { Id = 2, CreatedTime = DateTime.Now.AddDays(1), Description = "Gå en tur med hunden", Priority = PriorityLevel.Low, Completed = true },
            new TodoItem { Id = 3, CreatedTime = DateTime.Now.AddDays(-2), Description = "Købe ind", Priority = PriorityLevel.Normal, Completed = false }
            );
    }
}
