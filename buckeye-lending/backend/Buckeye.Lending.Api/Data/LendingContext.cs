using Microsoft.EntityFrameworkCore;
using Buckeye.Lending.Api.Models;

namespace Buckeye.Lending.Api.Data;

public class LendingContext : DbContext
{
    public LendingContext(DbContextOptions<LendingContext> options)
        : base(options) { }

    public DbSet<LoanApplicationDto> LoanApplications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoanApplicationDto>().HasData(
            new LoanApplicationDto { Id = 1, ApplicantName = "Sarah Johnson", LoanAmount = 250000m, LoanType = "Mortgage", AnnualIncome = 95000m, Status = "Approved", RiskRating = 2, SubmittedDate = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc), Notes = "Strong credit history, stable employment 8 years" },
            new LoanApplicationDto { Id = 2, ApplicantName = "Michael Chen", LoanAmount = 32500m, LoanType = "Auto", AnnualIncome = 68000m, Status = "Pending", RiskRating = 3, SubmittedDate = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc), Notes = "First-time auto loan, good income-to-debt ratio" },
            new LoanApplicationDto { Id = 3, ApplicantName = "Emily Rodriguez", LoanAmount = 320000m, LoanType = "Mortgage", AnnualIncome = 72000m, Status = "Denied", RiskRating = 5, SubmittedDate = new DateTime(2026, 1, 28, 0, 0, 0, DateTimeKind.Utc), Notes = "Income-to-loan ratio exceeds threshold" },
            new LoanApplicationDto { Id = 4, ApplicantName = "David Kim", LoanAmount = 15000m, LoanType = "Personal", AnnualIncome = 52000m, Status = "Approved", RiskRating = 2, SubmittedDate = new DateTime(2026, 2, 3, 0, 0, 0, DateTimeKind.Utc), Notes = "Consolidating credit card debt, excellent history" },
            new LoanApplicationDto { Id = 5, ApplicantName = "Jessica Martinez", LoanAmount = 500000m, LoanType = "Business", AnnualIncome = 150000m, Status = "Under Review", RiskRating = 4, SubmittedDate = new DateTime(2026, 2, 5, 0, 0, 0, DateTimeKind.Utc), Notes = "New restaurant venture, limited business credit" },
            new LoanApplicationDto { Id = 6, ApplicantName = "James Wilson", LoanAmount = 28000m, LoanType = "Auto", AnnualIncome = 75000m, Status = "Approved", RiskRating = 1, SubmittedDate = new DateTime(2026, 1, 20, 0, 0, 0, DateTimeKind.Utc), Notes = "Repeat customer, excellent credit score 780+" },
            new LoanApplicationDto { Id = 7, ApplicantName = "Amanda Foster", LoanAmount = 175000m, LoanType = "Mortgage", AnnualIncome = 88000m, Status = "Pending", RiskRating = 3, SubmittedDate = new DateTime(2026, 2, 10, 0, 0, 0, DateTimeKind.Utc), Notes = "First-time homebuyer, pending employment verification" },
            new LoanApplicationDto { Id = 8, ApplicantName = "Robert Taylor", LoanAmount = 75000m, LoanType = "Business", AnnualIncome = 120000m, Status = "Denied", RiskRating = 4, SubmittedDate = new DateTime(2026, 2, 8, 0, 0, 0, DateTimeKind.Utc), Notes = "Insufficient collateral for requested amount" }
        );
    }
}
