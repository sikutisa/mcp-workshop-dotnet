using Microsoft.EntityFrameworkCore;
using McpTodoServer.ContainerApp.Data;
using McpTodoServer.ContainerApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure high-performance in-memory database
builder.Services.AddDbContext<TodoDbContext>(options =>
{
    // Use EF Core InMemory provider for fast, ephemeral storage
    options.UseInMemoryDatabase("TodoDatabase")
           .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
           .EnableDetailedErrors(builder.Environment.IsDevelopment());
});

// Register Todo service with dependency injection
builder.Services.AddScoped<ITodoService, TodoService>();

// Add logging for performance monitoring
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

builder.Services.AddMcpServer()
                .WithHttpTransport(o => o.Stateless = true)
                .WithToolsFromAssembly();

var app = builder.Build();

// Initialize database and ensure it's created
await InitializeDatabaseAsync(app);

app.UseHttpsRedirection();

// Add health check endpoint to verify todo service
app.MapGet("/health", async (ITodoService todoService) =>
{
    try
    {
        var stats = await todoService.GetStatisticsAsync();
        var (avgTime, totalOps) = TodoService.GetPerformanceMetrics();
        
        return Results.Ok(new
        {
            Status = "Healthy",
            Database = "Connected",
            Statistics = new
            {
                Total = stats.Total,
                Completed = stats.Completed,
                Pending = stats.Pending
            },
            Performance = new
            {
                AverageQueryTimeMs = avgTime,
                TotalOperations = totalOps
            },
            Timestamp = DateTime.UtcNow
        });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Health check failed: {ex.Message}");
    }
});

// Add demo endpoint to test all 5 CRUD operations
app.MapPost("/demo", async (ITodoService todoService, ILogger<Program> logger) =>
{
    try
    {
        await McpTodoServer.ContainerApp.TodoServiceDemo.DemonstrateAllOperationsAsync(todoService, logger);
        
        var stats = await todoService.GetStatisticsAsync();
        var (avgTime, totalOps) = TodoService.GetPerformanceMetrics();
        
        return Results.Ok(new
        {
            Message = "✅ All 5 CRUD operations demonstrated successfully!",
            Operations = new[]
            {
                "1. CREATE - Added todo items",
                "2. LIST - Retrieved all todos", 
                "3. UPDATE - Modified todo text",
                "4. COMPLETE - Marked todo as done",
                "5. DELETE - Removed todo item"
            },
            FinalStatistics = new
            {
                Total = stats.Total,
                Completed = stats.Completed,
                Pending = stats.Pending
            },
            Performance = new
            {
                AverageQueryTimeMs = avgTime,
                TotalOperations = totalOps
            },
            Timestamp = DateTime.UtcNow
        });
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Demo failed");
        return Results.Problem($"Demo failed: {ex.Message}");
    }
});

app.MapMcp("/mcp");

app.Run();

/// <summary>
/// Initializes the database and ensures the schema is created.
/// </summary>
/// <param name="app">The web application instance.</param>
static async Task InitializeDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        // Ensure database is created for InMemory provider
        await context.Database.EnsureCreatedAsync();
        
        logger.LogInformation("✅ Todo database initialized successfully");
        logger.LogInformation("📊 Database provider: {Provider}", context.Database.ProviderName);
        logger.LogInformation("🗄️  Database connection: EF Core InMemory Database");
        
        // Log initial statistics
        var totalTodos = await context.TodoItems.CountAsync();
        logger.LogInformation("📋 Current todo items count: {Count}", totalTodos);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ Failed to initialize todo database");
        throw;
    }
}
