using McpTodoServer.ContainerApp.Models;
using McpTodoServer.ContainerApp.Services;
using ModelContextProtocol.Server;
using System.ComponentModel;


namespace McpTodoServer.ContainerApp.Tools;

/// <summary>
/// MCP Server Tool for todo list management operations.
/// Provides standardized MCP interface for creating, listing, updating, completing, and deleting todo items.
/// </summary>
[McpServerToolType]
public class TodoTool
{
    private readonly ITodoService _todoService;
    private readonly ILogger<TodoTool> _logger;

    /// <summary>
    /// Initializes a new instance of the TodoTool.
    /// </summary>
    /// <param name="todoService">The todo service for performing operations.</param>
    /// <param name="logger">Logger for operation tracking.</param>
    public TodoTool(ITodoService todoService, ILogger<TodoTool> logger)
    {
        _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a new todo item with the specified text.
    /// </summary>
    /// <param name="text">The text content for the new todo item.</param>
    /// <returns>The created todo item with assigned ID and timestamps.</returns>
    [McpServerTool(Name = "add_todo_item", Title = "Add a to-do item")]
    [Description("Adds a to-do item to database.")]
    public async Task<TodoItem> CreateAsync([Description("The text content for the todo item")] string text)
    {
        _logger.LogInformation("MCP Tool: Creating todo item with text: {Text}", text);
        
        var result = await _todoService.CreateTodoAsync(text);
        
        _logger.LogInformation("MCP Tool: Successfully created todo item {Id}", result.Id);
        return result;
    }

    /// <summary>
    /// Retrieves all todo items from the database.
    /// </summary>
    /// <returns>A list of all todo items.</returns>
    [McpServerTool(Name = "get_todo_items", Title = "Get a list of to-do items")]
    [Description("Gets a list of to-do items from database.")]
    public async Task<List<TodoItem>> ListAsync()
    {
        _logger.LogInformation("MCP Tool: Retrieving all todo items");
        
        var result = await _todoService.GetTodosAsync();
        
        _logger.LogInformation("MCP Tool: Retrieved {Count} todo items", result.Count);
        return result.ToList();
    }

    /// <summary>
    /// Updates the text content of an existing todo item.
    /// </summary>
    /// <param name="id">The ID of the todo item to update.</param>
    /// <param name="text">The new text content for the todo item.</param>
    /// <returns>The updated todo item, or null if not found.</returns>
    [McpServerTool(Name = "update_todo_item", Title = "Update a to-do item")]
    [Description("Updates a to-do item in the database.")]
    public async Task<TodoItem?> UpdateAsync(
        [Description("The ID of the todo item to update")] int id,
        [Description("The new text content for the todo item")] string text)
    {
        _logger.LogInformation("MCP Tool: Updating todo item {Id} with new text: {Text}", id, text);
        
        var result = await _todoService.UpdateTodoAsync(id, text);
        
        if (result != null)
        {
            _logger.LogInformation("MCP Tool: Successfully updated todo item {Id}", id);
        }
        else
        {
            _logger.LogWarning("MCP Tool: Todo item {Id} not found for update", id);
        }
        
        return result;
    }

    /// <summary>
    /// Marks a todo item as completed.
    /// </summary>
    /// <param name="id">The ID of the todo item to complete.</param>
    /// <returns>The completed todo item, or null if not found.</returns>
    [McpServerTool(Name = "complete_todo_item", Title = "Complete a to-do item")]
    [Description("Completes a to-do item in the database.")]
    public async Task<TodoItem?> CompleteAsync([Description("The ID of the todo item to complete")] int id)
    {
        _logger.LogInformation("MCP Tool: Completing todo item {Id}", id);
        
        var result = await _todoService.CompleteTodoAsync(id, true);
        
        if (result != null)
        {
            _logger.LogInformation("MCP Tool: Successfully completed todo item {Id}", id);
        }
        else
        {
            _logger.LogWarning("MCP Tool: Todo item {Id} not found for completion", id);
        }
        
        return result;
    }

    /// <summary>
    /// Deletes a todo item from the database.
    /// </summary>
    /// <param name="id">The ID of the todo item to delete.</param>
    /// <returns>True if the item was deleted successfully, false if not found.</returns>
    [McpServerTool(Name = "delete_todo_item", Title = "Delete a to-do item")]
    [Description("Deletes a to-do item from the database.")]
    public async Task<bool> DeleteAsync([Description("The ID of the todo item to delete")] int id)
    {
        _logger.LogInformation("MCP Tool: Deleting todo item {Id}", id);
        
        var result = await _todoService.DeleteTodoAsync(id);
        
        if (result)
        {
            _logger.LogInformation("MCP Tool: Successfully deleted todo item {Id}", id);
        }
        else
        {
            _logger.LogWarning("MCP Tool: Todo item {Id} not found for deletion", id);
        }
        
        return result;
    }
}
