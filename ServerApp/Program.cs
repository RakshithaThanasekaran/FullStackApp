var builder = WebApplication.CreateBuilder(args);

// Register CORS services
builder.Services.AddCors();

var app = builder.Build();

// Enable CORS with open policy so the front-end can call the API
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.MapGet("/api/productlist", () =>
{
    return new[]
    {
        new { Id = 1, Name = "Laptop", Price = 1200.50, Stock = 25 },
        new { Id = 2, Name = "Headphones", Price = 50.00, Stock = 100 }
    };
});

app.Run();
