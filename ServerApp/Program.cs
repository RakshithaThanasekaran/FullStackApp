using Microsoft.Extensions.Caching.Memory;

record Category(int Id, string Name);
record Product(int Id, string Name, double Price, int Stock, Category Category);

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors();
        builder.Services.AddMemoryCache();

        var app = builder.Build();

        app.UseCors(policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());

        var cache = app.Services.GetRequiredService<IMemoryCache>();

        app.MapGet("/api/productlist", () =>
        {
            return cache.GetOrCreate("productListCacheKey", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return new[]
                {
                    new Product(1, "Laptop", 1200.50, 25, new Category(101, "Electronics")),
                    new Product(2, "Headphones", 50.00, 100, new Category(102, "Accessories"))
                };
            });
        });

        app.Run();
    }
}
