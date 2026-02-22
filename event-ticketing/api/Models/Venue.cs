using System.ComponentModel.DataAnnotations;

namespace EventsApi.Models;

/// <summary>
/// Physical location where events are held — one venue hosts many events.
/// </summary>
public class Venue
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    public int Capacity { get; set; }

    // Navigation — one venue hosts many events
    public List<Event> Events { get; set; } = [];
}
