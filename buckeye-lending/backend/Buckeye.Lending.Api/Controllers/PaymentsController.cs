using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Buckeye.Lending.Api.Data;
using Buckeye.Lending.Api.Models;

namespace Buckeye.Lending.Api.Controllers;

[ApiController]
[Route("api/loanapplications/{loanApplicationId}/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly LendingContext _context;

    public PaymentsController(LendingContext context)
    {
        _context = context;
    }

    /// <summary>Get all payments for a loan application.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LoanPayment>>> GetAll(int loanApplicationId)
    {
        var payments = await _context.LoanPayments
            .Where(p => p.LoanApplicationId == loanApplicationId)
            .ToListAsync();

        return Ok(payments);
    }

    /// <summary>Record a new payment against a loan application.</summary>
    [HttpPost]
    public async Task<ActionResult<LoanPayment>> Create(int loanApplicationId, LoanPayment payment)
    {
        var app = await _context.LoanApplications.FindAsync(loanApplicationId);
        if (app == null)
            throw new KeyNotFoundException($"Loan application with ID {loanApplicationId} not found");

        payment.LoanApplicationId = loanApplicationId;
        payment.PaymentDate = DateTime.UtcNow;

        _context.LoanPayments.Add(payment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { loanApplicationId }, payment);
    }
}
