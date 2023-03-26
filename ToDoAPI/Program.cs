using Microsoft.EntityFrameworkCore;
using ToDoDataAccess.Context;
using ToDoDataAccess.Repository;
using ToDoDomain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register Context
builder.Services.AddDbContext<ToDoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Register Repositories
builder.Services.AddScoped<IItemRepository, ItemRepository>();

// Register Services
builder.Services.AddScoped<IItemService, ItemService>();

// Register Controllers
builder.Services.AddControllers();  

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Add Migrations
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var dbContext = services.GetService<ToDoContext>();
dbContext.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
