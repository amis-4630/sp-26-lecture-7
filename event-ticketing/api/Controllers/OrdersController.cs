using EventsApi.Data;
using EventsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.Controllers;

[ApiController]
[Route("api/events/{eventId}/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly EventsContext _context;

    public OrdersController(EventsContext context)
    {
        _context = context;
    }

    /// <summary>Get all ticket orders for an event.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketOrder>>> GetAll(int eventId)
    {
        var orders = await _context.TicketOrders
            .Where(o => o.EventId == eventId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        return Ok(orders);
    }

    /// <summary>Place a new ticket order for an event.</summary>
    [HttpPost]
    public async Task<ActionResult<TicketOrder>> Create(int eventId, TicketOrder order)
    {
        var ev = await _context.Events.FindAsync(eventId);
        if (ev == null)
            return NotFound($"Event with ID {eventId} not found");

        if (order.Quantity <= 0)
            return BadRequest("Quantity must be at least 1");

        if (order.Quantity > ev.AvailableTickets)
            return BadRequest($"Only {ev.AvailableTickets} tickets available");

        // Calculate total and reduce available tickets
        order.EventId = eventId;
        order.TotalPrice = order.Quantity * ev.Price;
        order.OrderDate = DateTime.UtcNow;
        ev.AvailableTickets -= order.Quantity;

        _context.TicketOrders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { eventId }, order);
    }
}
