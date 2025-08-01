using Microsoft.EntityFrameworkCore;
using McpTodoServer.ContainerApp.Models;

namespace McpTodoServer.ContainerApp.Data;

/// <summary>
/// Database context for managing TodoItem entities with high-performance configurations.
/// Uses in-memory SQLite for fast, ephemeral storage perfect for microservices.
/// </summary>
public class TodoDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the collection of TodoItem entities.
    /// </summary>
    public DbSet<TodoItem> TodoItems { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the TodoDbContext with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the model and entity relationships.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure TodoItem entity
        modelBuilder.Entity<TodoItem>(entity =>
        {
            // Primary key
            entity.HasKey(e => e.Id);

            // Configure Id as auto-incrementing
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            // Configure Text property
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(500);

            // Configure IsCompleted with default value
            entity.Property(e => e.IsCompleted)
                .HasDefaultValue(false);

            // Configure timestamps
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Index for performance on common queries
            entity.HasIndex(e => e.IsCompleted)
                .HasDatabaseName("IX_TodoItems_IsCompleted");

            entity.HasIndex(e => e.CreatedAt)
                .HasDatabaseName("IX_TodoItems_CreatedAt");
        });
    }

    /// <summary>
    /// Override SaveChanges to automatically update timestamps.
    /// </summary>
    /// <returns>The number of entities saved.</returns>
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    /// <summary>
    /// Override SaveChangesAsync to automatically update timestamps.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of entities saved.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates the UpdatedAt timestamp for modified entities.
    /// </summary>
    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries<TodoItem>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
