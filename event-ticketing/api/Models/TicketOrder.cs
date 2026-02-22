using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsApi.Models;

/// <summary>
/// Represents a ticket purchase for an event — many orders per event.
/// </summary>
public class TicketOrder
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string CustomerEmail { get; set; } = string.Empty;

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalPrice { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    // Foreign key — each order belongs to one event
    public int EventId { get; set; }

    // Navigation property
    public Event? Event { get; set; }
}
