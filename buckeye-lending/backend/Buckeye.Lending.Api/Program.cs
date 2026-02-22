using Microsoft.EntityFrameworkCore;
using Buckeye.Lending.Api.Data;
using Buckeye.Lending.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Problem Details support (RFC 7807)
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        // Add machine name for diagnostics (only in development)
        if (builder.Environment.IsDevelopment())
        {
            context.ProblemDetails.Extensions["machine"] = Environment.MachineName;
        }
    };
});

// Add global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// EF Core — InMemory provider
builder.Services.AddDbContext<LendingContext>(options =>
    options.UseInMemoryDatabase("BuckeyeLending"));

var app = builder.Build();

// Initialize on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LendingContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Buckeye Lending API v1");
    });
}

// Use exception handler middleware
app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
