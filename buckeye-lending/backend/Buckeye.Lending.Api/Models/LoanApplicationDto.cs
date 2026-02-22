using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buckeye.Lending.Api.Models;

public class LoanApplicationDto
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string ApplicantName { get; set; } = string.Empty;

    [Required, Column(TypeName = "decimal(12,2)")]
    public decimal LoanAmount { get; set; }

    [Required, MaxLength(50)]
    public string LoanType { get; set; } = string.Empty;

    [Column(TypeName = "decimal(12,2)")]
    public decimal AnnualIncome { get; set; }

    [Required, MaxLength(30)]
    public string Status { get; set; } = "Pending";

    [Range(1, 5)]
    public int RiskRating { get; set; }

    public DateTime SubmittedDate { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string Notes { get; set; } = string.Empty;
}
