using EventsApi.Data;
using EventsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly EventsContext _context;

    public EventsController(EventsContext context)
    {
        _context = context;
    }

    // GET api/events?categoryId=1&search=ohio
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetAll(
        [FromQuery] int? categoryId,
        [FromQuery] string? search)
    {
        var query = _context.Events
            .Include(e => e.Venue)
            .Include(e => e.EventCategory)
            .AsQueryable();

        if (categoryId.HasValue)
            query = query.Where(e => e.EventCategoryId == categoryId.Value);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(e => e.Title.Contains(search, StringComparison.OrdinalIgnoreCase));

        return Ok(await query.ToListAsync());
    }

    // GET api/events/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetById(int id)
    {
        var ev = await _context.Events
            .Include(e => e.Venue)
            .Include(e => e.EventCategory)
            .Include(e => e.TicketOrders)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (ev == null)
            return NotFound();

        return Ok(ev);
    }
}
