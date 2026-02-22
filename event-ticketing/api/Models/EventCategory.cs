using System.ComponentModel.DataAnnotations;

namespace EventsApi.Models;

/// <summary>
/// Lookup entity for event categories (Sports, Music, Food, etc.).
/// </summary>
public class EventCategory
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    // Navigation — one category contains many events
    public List<Event> Events { get; set; } = [];
}
