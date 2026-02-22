using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Buckeye.Lending.Api.Data;
using Buckeye.Lending.Api.Models;

namespace Buckeye.Lending.Api.Controllers;

[ApiController]
[Route("api/loanapplications/{loanApplicationId}/[controller]")]
public class NotesController : ControllerBase
{
    private readonly LendingContext _context;

    public NotesController(LendingContext context)
    {
        _context = context;
    }

    /// <summary>Get all notes for a loan application.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LoanNote>>> GetAll(int loanApplicationId)
    {
        var notes = await _context.LoanNotes
            .Where(n => n.LoanApplicationId == loanApplicationId)
            .OrderBy(n => n.CreatedDate)
            .ToListAsync();

        return Ok(notes);
    }

    /// <summary>Add a note to a loan application.</summary>
    [HttpPost]
    public async Task<ActionResult<LoanNote>> Create(int loanApplicationId, LoanNote note)
    {
        var app = await _context.LoanApplications.FindAsync(loanApplicationId);
        if (app == null)
            throw new KeyNotFoundException($"Loan application with ID {loanApplicationId} not found");

        note.LoanApplicationId = loanApplicationId;
        note.CreatedDate = DateTime.UtcNow;

        _context.LoanNotes.Add(note);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { loanApplicationId }, note);
    }
}
