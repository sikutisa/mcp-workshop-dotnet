using McpTodoServer.ContainerApp.Models;

namespace McpTodoServer.ContainerApp.Services;

/// <summary>
/// Interface for managing todo list operations with high-performance async methods.
/// Provides the 5 core features: create, list, update, complete, and delete.
/// </summary>
public interface ITodoService
{
    /// <summary>
    /// Creates a new todo item with the specified text.
    /// </summary>
    /// <param name="text">The text description of the todo item.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The created TodoItem with generated ID and timestamps.</returns>
    Task<TodoItem> CreateTodoAsync(string text, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all todo items with optional filtering by completion status.
    /// </summary>
    /// <param name="isCompleted">Optional filter by completion status. If null, returns all items.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>A list of TodoItem entities matching the criteria.</returns>
    Task<IReadOnlyList<TodoItem>> GetTodosAsync(bool? isCompleted = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a specific todo item by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the todo item.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The TodoItem if found, otherwise null.</returns>
    Task<TodoItem?> GetTodoByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the text of an existing todo item.
    /// </summary>
    /// <param name="id">The unique identifier of the todo item to update.</param>
    /// <param name="newText">The new text description for the todo item.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The updated TodoItem if found, otherwise null.</returns>
    Task<TodoItem?> UpdateTodoAsync(int id, string newText, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks a todo item as completed or incomplete.
    /// </summary>
    /// <param name="id">The unique identifier of the todo item to complete.</param>
    /// <param name="isCompleted">The completion status to set (default: true).</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The updated TodoItem if found, otherwise null.</returns>
    Task<TodoItem?> CompleteTodoAsync(int id, bool isCompleted = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a todo item by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the todo item to delete.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>True if the item was found and deleted, otherwise false.</returns>
    Task<bool> DeleteTodoAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets performance and collection statistics for monitoring.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>A tuple containing total count, completed count, and pending count.</returns>
    Task<(int Total, int Completed, int Pending)> GetStatisticsAsync(CancellationToken cancellationToken = default);
}
