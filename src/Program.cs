using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinhaBackend2023Q3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

// Setup database context
builder.Services.AddDbContext<RinhaContext>(
    options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseNpgsql(connectionString);
    }
    );

// Disable ModelState validation (Validation will be done on the Controller)
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Migrate database if needed
using (var scope = app.Services.CreateScope())
{
    var _context = scope.ServiceProvider.GetService<RinhaContext>();
    try
    {
        _context.Database.Migrate();
    }
    catch
    {
        // Do nothing if it fails
    }
}

app.MapControllers();

app.Run();
