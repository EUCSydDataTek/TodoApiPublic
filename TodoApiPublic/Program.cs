using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS er n�dvendig af hensyn til Blazor WASM clienten
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    builder.WithOrigins("https://localhost:7159")
    .AllowAnyHeader()
    .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ }

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

#region GET: /
app.MapGet("/", () => "Hello World!");
#endregion

#region GET: /todoitems
app.MapGet("/todoitems", async (ITodoService service) =>
{
    var todoItems = await service.GetAsync();
    return todoItems;
});
#endregion

#region GET: /todoitems/all
app.MapGet("/todoitems/all", async (ITodoService service) =>
{
    var todoItems = await service.GetAllAsync();
    return todoItems;
});
#endregion

#region GET: /todoitems/5
app.MapGet("/todoitems/{id}", async (int id, ITodoService service) =>
    await service.GetByIdAsync(id)
       is TodoItem todoItem
           ? Results.Ok(todoItem)
           : Results.NotFound());
#endregion

#region POST: /todoitems
app.MapPost("/todoitems", async (TodoItem todoItem, ITodoService service) =>
{
    await service.CreateAsync(todoItem);
    return Results.Created($"/todoitems/{todoItem.Id}", todoItem);
});
#endregion

#region PUT: /todoitems/5
app.MapPut("/todoitems/{id}", async (int id, TodoItem inputTodoItem, ITodoService service) =>
{
    var todoItem = await service.GetByIdAsync(id);

    if (todoItem is null) return Results.NotFound();

    todoItem.Description = inputTodoItem.Description;
    todoItem.Priority = inputTodoItem.Priority;
    todoItem.Completed = inputTodoItem.Completed;

    await service.Update(id, todoItem);

    return Results.NoContent();
});
#endregion

#region DELETE: /todoitems/5
app.MapDelete("/todoitems/{id}", async (int id, ITodoService service) =>
{
    if (await service.GetByIdAsync(id) is TodoItem todoItem)
    {
        await service.Delete(todoItem.Id);
        return Results.NoContent();
    }

    return Results.NotFound();
});
#endregion

app.Run();


