/* FORMAT
 app.Map[HTTP_METHOD]("ROUTE", (parameters) =>
{
    // logic

    return response;
});*/

using Microsoft.EntityFrameworkCore; //EF classes - DbContext, DbSet, UseSqlite(), ToListAsync()

var builder = WebApplication.CreateBuilder(args); //Create application builder

builder.Services.AddEndpointsApiExplorer(); //API documentation services
builder.Services.AddSwaggerGen(); //Adds Swagger/OpenAPI generation
builder.Services.AddCors();

//tell EF to use SQLite with a file called todos.db
// 1. Register TodoDb with Dependency Injection
// 2. Configure Entity Framework to use SQLite
// 3. A file named todos.db will be created automatically
//builder.Services.AddDbContext<TodoDb>(opt => opt.UseSqlite("Data Source=todos.db"));
builder.Services.AddDbContext<TodoDb>(opt => opt.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TodoDb;Trusted_Connection=True;TrustServerCertificate=True;"));

var app = builder.Build(); // Build the application using all configured services

app.UseSwagger(); // Enable Swagger JSON endpoint
app.UseSwaggerUI(); // Enable Swagger UI in the browser
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//GET - get all todos
app.MapGet("/todos", async (TodoDb db) => Results.Ok(await db.Todos.ToListAsync())); //SQL - SELECT * FROM Todos; (await db.Todos.ToListAsync()) then will return result Ok with json data

//POST - add new todo
app.MapPost("/todos", async (TodoItem todo, TodoDb db) => 
{
    db.Todos.Add(todo); //SQL - INSERT INTO Todos
    await db.SaveChangesAsync(); //Actually executes SQL against database - save into db
    return Results.Created($"/todos/{todo.Id}", todo); //
});

//PUT - mark complete
app.MapPut("/todos/{id}", async (int id, TodoDb db) => //Create endpoint id
{
    var todo = await db.Todos.FindAsync(id); //SQL - SELECT * FROM Todos WHERE Id = 1;
    if (todo == null) return Results.NotFound("Task not found!"); //if not exist return
    todo.IsComplete = true; //update record
    await db.SaveChangesAsync(); //save update - UPDATE Todos     SET IsComplete = 1    WHERE Id = 1;
    return Results.Ok(todo);
});

//DELETE - remove task
app.MapDelete("/todos/{id}", async (int id, TodoDb db) => 
{
    var todo = await db.Todos.FindAsync(id); //search by id
    if(todo == null) return Results.NotFound("Task not found!");
    db.Todos.Remove(todo); //remove todo
    await db.SaveChangesAsync(); //save result
    return Results.Ok("Task deleted!");
});

// Database Initialization - Creates the database and tables if they don't exist
using (var scope = app.Services.CreateScope()) //Creates a temporary service scope
{
    var db = scope.ServiceProvider.GetRequiredService<TodoDb>(); //Gets database context from Dependency Injection
    db.Database.EnsureCreated(); //cheack exit or not - if not will create database and table
}

/*
//our data - stored in memory for now
var todos = new List<TodoItem>();

//GET all todos
app.MapGet("/todos", () =>
{
    return Results.Ok(todos);
});

//POST - add new todo
app.MapPost("/todos", (TodoItem todo) =>
{
    todo.Id = todos.Count + 1; //auto generate id
    todos.Add(todo);
    return Results.Created($"/todos/{todo.Id}", todo);
});

//PUT - mark complete
app.MapPut("/todos/{id}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    if (todo == null) return Results.NotFound("Task not found!");
    todo.IsComplete = true;
    return Results.Ok(todo);
});

//DELETE - remove task
app.MapDelete("/todos/{id}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    if (todo == null) return Results.NotFound("Task not found!");
    todos.Remove(todo);
    return Results.Ok("Task deleted!");
});
//{id} is a route parameter - it captures whatever number the user puts in the url
*/
app.Run(); //start web browser

//TodoItem class
class TodoItem
{
    public int Id { get; set;}
    public string? Name { get; set;}
    public bool IsComplete { get; set;}
}

//DBContext - the bridge between C# and database
class TodoDb : DbContext //DBContext - The bridge between C# objects and database tables
{
    public TodoDb(DbContextOptions<TodoDb> options) : base(options) { }  // Constructor receives database configuration
    public DbSet<TodoItem> Todos { get; set; } // Represents the Todos table in SQLite
}

/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Add these with other builder.Services lines:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Add these after app is built:
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray(); 
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/