using EventsApi.Data;
using EventsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventCategoriesController : ControllerBase
{
    private readonly EventsContext _context;

    public EventCategoriesController(EventsContext context)
    {
        _context = context;
    }

    /// <summary>Get all event categories.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventCategory>>> GetAll()
    {
        return Ok(await _context.EventCategories.ToListAsync());
    }

    /// <summary>Get a category by ID.</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<EventCategory>> GetById(int id)
    {
        var category = await _context.EventCategories.FindAsync(id);
        if (category == null)
            return NotFound();

        return Ok(category);
    }
}
