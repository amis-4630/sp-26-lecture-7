using Microsoft.EntityFrameworkCore;
using EventsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Prevent circular reference errors from navigation properties
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core — InMemory provider
builder.Services.AddDbContext<EventsContext>(options =>
    options.UseInMemoryDatabase("EventTicketing"));

// Configure CORS to allow requests from the React frontend
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Initialize seed data on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EventsContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.MapControllers();

app.Run();

