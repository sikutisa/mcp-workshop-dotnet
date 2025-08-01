namespace MyMonkeyApp;

/// <summary>
/// Represents a monkey species with detailed information about its characteristics and habitat.
/// </summary>
public class Monkey
{
    /// <summary>
    /// Gets or sets the name of the monkey species.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the primary location where this monkey species is found.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated population of this monkey species.
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// Gets or sets additional details about the monkey species.
    /// </summary>
    public string Details { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL or filename for the monkey species.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Returns a formatted string representation of the monkey species.
    /// </summary>
    /// <returns>A string containing the monkey's name, location, and population.</returns>
    public override string ToString()
    {
        return $"{Name} - {Location} (Population: {Population:N0})";
    }
}