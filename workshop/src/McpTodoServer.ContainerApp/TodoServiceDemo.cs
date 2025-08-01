using McpTodoServer.ContainerApp.Services;
using McpTodoServer.ContainerApp.Models;

namespace McpTodoServer.ContainerApp;

/// <summary>
/// Demonstration class to showcase all 5 CRUD operations of the TodoService.
/// This class demonstrates: Create, List, Update, Complete, and Delete operations.
/// </summary>
public static class TodoServiceDemo
{
    /// <summary>
    /// Demonstrates all todo management operations with performance monitoring.
    /// </summary>
    /// <param name="todoService">The todo service instance.</param>
    /// <param name="logger">Logger for output.</param>
    public static async Task DemonstrateAllOperationsAsync(ITodoService todoService, ILogger logger)
    {
        logger.LogInformation("🚀 Starting Todo Service Demonstration - All 5 CRUD Operations");
        
        try
        {
            // 1. CREATE - Add new todo items
            logger.LogInformation("\n📝 1. CREATE Operations");
            var todo1 = await todoService.CreateTodoAsync("Learn ASP.NET Core Minimal APIs");
            var todo2 = await todoService.CreateTodoAsync("Implement EF Core with InMemory Database");
            var todo3 = await todoService.CreateTodoAsync("Build high-performance todo service");
            
            logger.LogInformation("✅ Created todo: {Text} (ID: {Id})", todo1.Text, todo1.Id);
            logger.LogInformation("✅ Created todo: {Text} (ID: {Id})", todo2.Text, todo2.Id);
            logger.LogInformation("✅ Created todo: {Text} (ID: {Id})", todo3.Text, todo3.Id);

            // 2. LIST - Get all todos
            logger.LogInformation("\n📋 2. LIST Operations");
            var allTodos = await todoService.GetTodosAsync();
            logger.LogInformation("📊 Total todos: {Count}", allTodos.Count());
            foreach (var todo in allTodos)
            {
                logger.LogInformation("  • {Text} - {Status} (Created: {Created})", 
                    todo.Text, 
                    todo.IsCompleted ? "✅ Completed" : "⏳ Pending",
                    todo.CreatedAt.ToString("HH:mm:ss"));
            }

            // 3. UPDATE - Modify existing todo
            logger.LogInformation("\n✏️ 3. UPDATE Operations");
            var updatedTodo = await todoService.UpdateTodoAsync(todo2.Id, "Master Entity Framework Core with performance optimization");
            if (updatedTodo != null)
            {
                logger.LogInformation("✅ Updated todo {Id}: '{OldText}' → '{NewText}'", 
                    todo2.Id, todo2.Text, updatedTodo.Text);
            }

            // 4. COMPLETE - Mark todo as completed
            logger.LogInformation("\n🎯 4. COMPLETE Operations");
            var completedTodo = await todoService.CompleteTodoAsync(todo1.Id);
            if (completedTodo != null)
            {
                logger.LogInformation("✅ Completed todo {Id}: '{Text}' - Status: {Status}", 
                    completedTodo.Id, completedTodo.Text, completedTodo.IsCompleted ? "Done" : "Pending");
            }

            // 5. DELETE - Remove todo
            logger.LogInformation("\n🗑️ 5. DELETE Operations");
            var deleteResult = await todoService.DeleteTodoAsync(todo3.Id);
            logger.LogInformation("✅ Deleted todo {Id}: Success = {Success}", todo3.Id, deleteResult);

            // Final statistics
            logger.LogInformation("\n📊 Final Statistics");
            var stats = await todoService.GetStatisticsAsync();
            logger.LogInformation("📈 Total: {Total}, Completed: {Completed}, Pending: {Pending}", 
                stats.Total, stats.Completed, stats.Pending);

            // Performance metrics
            var (avgTime, totalOps) = TodoService.GetPerformanceMetrics();
            logger.LogInformation("⚡ Performance: Avg {AvgTime:F2}ms per operation, {TotalOps} total operations", 
                avgTime, totalOps);

            logger.LogInformation("\n🎉 Todo Service Demonstration Completed Successfully!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Error during todo service demonstration");
            throw;
        }
    }
}
