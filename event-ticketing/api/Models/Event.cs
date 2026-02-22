using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsApi.Models;

public class Event
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string Date { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public int AvailableTickets { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    // Foreign key — which venue hosts this event
    public int VenueId { get; set; }
    public Venue? Venue { get; set; }

    // Foreign key — what category of event
    public int EventCategoryId { get; set; }
    public EventCategory? EventCategory { get; set; }

    // Navigation — one event can have many ticket orders
    public List<TicketOrder> TicketOrders { get; set; } = [];
}
