using Microsoft.EntityFrameworkCore;
using McpTodoServer.ContainerApp.Data;
using McpTodoServer.ContainerApp.Models;
using System.Diagnostics;

namespace McpTodoServer.ContainerApp.Services;

/// <summary>
/// High-performance implementation of todo list management service with optimized database operations.
/// Implements performance monitoring, efficient queries, and proper async patterns.
/// </summary>
public class TodoService : ITodoService
{
    private readonly TodoDbContext _context;
    private readonly ILogger<TodoService> _logger;
    private static long _totalOperations = 0;
    private static long _totalQueryTime = 0;

    /// <summary>
    /// Initializes a new instance of the TodoService.
    /// </summary>
    /// <param name="context">The database context for todo operations.</param>
    /// <param name="logger">Logger for performance monitoring and debugging.</param>
    public TodoService(TodoDbContext context, ILogger<TodoService> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<TodoItem> CreateTodoAsync(string text, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text, nameof(text));

        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var todoItem = new TodoItem
            {
                Text = text.Trim(),
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Created todo item {Id} with text: {Text}", todoItem.Id, todoItem.Text);
            return todoItem;
        }
        finally
        {
            stopwatch.Stop();
            TrackPerformance("CreateTodo", stopwatch.ElapsedMilliseconds);
        }
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<TodoItem>> GetTodosAsync(bool? isCompleted = null, CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            IQueryable<TodoItem> query = _context.TodoItems.AsNoTracking();

            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            // Order by created date for consistent results
            query = query.OrderBy(t => t.CreatedAt);

            var todos = await query.ToListAsync(cancellationToken);
            
            _logger.LogDebug("Retrieved {Count} todo items (completed filter: {Filter})", 
                todos.Count, isCompleted?.ToString() ?? "none");
            
            return todos.AsReadOnly();
        }
        finally
        {
            stopwatch.Stop();
            TrackPerformance("GetTodos", stopwatch.ElapsedMilliseconds);
        }
    }

    /// <inheritdoc />
    public async Task<TodoItem?> GetTodoByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var todo = await _context.TodoItems
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (todo != null)
            {
                _logger.LogDebug("Found todo item {Id}", id);
            }
            else
            {
                _logger.LogDebug("Todo item {Id} not found", id);
            }

            return todo;
        }
        finally
        {
            stopwatch.Stop();
            TrackPerformance("GetTodoById", stopwatch.ElapsedMilliseconds);
        }
    }

    /// <inheritdoc />
    public async Task<TodoItem?> UpdateTodoAsync(int id, string newText, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newText, nameof(newText));

        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var todo = await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (todo == null)
            {
                _logger.LogWarning("Todo item {Id} not found for update", id);
                return null;
            }

            var oldText = todo.Text;
            todo.Text = newText.Trim();
            todo.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Updated todo item {Id}: '{OldText}' -> '{NewText}'", 
                id, oldText, todo.Text);
            
            return todo;
        }
        finally
        {
            stopwatch.Stop();
            TrackPerformance("UpdateTodo", stopwatch.ElapsedMilliseconds);
        }
    }

    /// <inheritdoc />
    public async Task<TodoItem?> CompleteTodoAsync(int id, bool isCompleted = true, CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var todo = await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (todo == null)
            {
                _logger.LogWarning("Todo item {Id} not found for completion", id);
                return null;
            }

            if (todo.IsCompleted == isCompleted)
            {
                _logger.LogDebug("Todo item {Id} already has completion status {Status}", id, isCompleted);
                return todo;
            }

            todo.IsCompleted = isCompleted;
            todo.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Todo item {Id} marked as {Status}: {Text}", 
                id, isCompleted ? "completed" : "pending", todo.Text);
            
            return todo;
        }
        finally
        {
            stopwatch.Stop();
            TrackPerformance("CompleteTodo", stopwatch.ElapsedMilliseconds);
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteTodoAsync(int id, CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var todo = await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (todo == null)
            {
                _logger.LogWarning("Todo item {Id} not found for deletion", id);
                return false;
            }

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Deleted todo item {Id}: {Text}", id, todo.Text);
            return true;
        }
        finally
        {
            stopwatch.Stop();
            TrackPerformance("DeleteTodo", stopwatch.ElapsedMilliseconds);
        }
    }

    /// <inheritdoc />
    public async Task<(int Total, int Completed, int Pending)> GetStatisticsAsync(CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            // Use efficient aggregate queries
            var stats = await _context.TodoItems
                .AsNoTracking()
                .GroupBy(t => 1) // Group all items together
                .Select(g => new
                {
                    Total = g.Count(),
                    Completed = g.Count(t => t.IsCompleted),
                    Pending = g.Count(t => !t.IsCompleted)
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (stats == null)
            {
                return (0, 0, 0);
            }

            _logger.LogDebug("Statistics: Total={Total}, Completed={Completed}, Pending={Pending}", 
                stats.Total, stats.Completed, stats.Pending);

            return (stats.Total, stats.Completed, stats.Pending);
        }
        finally
        {
            stopwatch.Stop();
            TrackPerformance("GetStatistics", stopwatch.ElapsedMilliseconds);
        }
    }

    /// <summary>
    /// Tracks performance metrics for monitoring and optimization.
    /// </summary>
    /// <param name="operation">The name of the operation.</param>
    /// <param name="elapsedMs">The elapsed time in milliseconds.</param>
    private void TrackPerformance(string operation, long elapsedMs)
    {
        Interlocked.Increment(ref _totalOperations);
        Interlocked.Add(ref _totalQueryTime, elapsedMs);

        var avgTime = _totalOperations > 0 ? _totalQueryTime / _totalOperations : 0;

        _logger.LogDebug("Performance: {Operation} took {ElapsedMs}ms (Avg: {AvgMs}ms, Total ops: {TotalOps})", 
            operation, elapsedMs, avgTime, _totalOperations);

        // Log slow operations for performance monitoring
        if (elapsedMs > 100) // More than 100ms is considered slow
        {
            _logger.LogWarning("Slow operation detected: {Operation} took {ElapsedMs}ms", operation, elapsedMs);
        }
    }

    /// <summary>
    /// Gets the current performance metrics.
    /// </summary>
    /// <returns>A tuple containing average query time and total operations.</returns>
    public static (long AvgQueryTimeMs, long TotalOperations) GetPerformanceMetrics()
    {
        var avgTime = _totalOperations > 0 ? _totalQueryTime / _totalOperations : 0;
        return (avgTime, _totalOperations);
    }
}
