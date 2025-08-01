using System.ComponentModel.DataAnnotations;

namespace McpTodoServer.ContainerApp.Models;

/// <summary>
/// Represents a to-do item with essential properties.
/// </summary>
public class TodoItem
{
    /// <summary>
    /// Unique identifier for the to-do item.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// The text description of the to-do item.
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the to-do item has been completed.
    /// </summary>
    public bool IsCompleted { get; set; } = false;

    /// <summary>
    /// Timestamp when the item was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Timestamp when the item was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
