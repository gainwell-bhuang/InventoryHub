using InventoryHub.shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              //.WithOrigins("*")
              //.WithOrigins("http://localhost:5161") // Replace with your front-end URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS middleware
app.UseCors();

app.MapGet("/api/productlist", () =>
{
    return new[]
    {
        new Product
        {
            Id = 1,
            Name = "Laptop",
            Price = 1200.50,
            Stock = 25,
            Category = new Category { Id = 101, Name = "Electronics" }
        },
        new Product
        {
            Id = 2,
            Name = "Headphones",
            Price = 50.00,
            Stock = 100,
            Category = new Category { Id = 102, Name = "Accessories" }
        }
    };
})
.WithName("GetProductList")
.WithOpenApi();

app.Run();