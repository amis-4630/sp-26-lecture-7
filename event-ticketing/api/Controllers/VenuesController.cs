using EventsApi.Data;
using EventsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VenuesController : ControllerBase
{
    private readonly EventsContext _context;

    public VenuesController(EventsContext context)
    {
        _context = context;
    }

    /// <summary>Get all venues.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venue>>> GetAll()
    {
        return Ok(await _context.Venues.ToListAsync());
    }

    /// <summary>Get a venue by ID, including its events.</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Venue>> GetById(int id)
    {
        var venue = await _context.Venues
            .Include(v => v.Events)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (venue == null)
            return NotFound();

        return Ok(venue);
    }
}
