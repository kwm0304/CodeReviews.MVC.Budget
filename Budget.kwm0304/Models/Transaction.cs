using System.ComponentModel.DataAnnotations;

namespace Budget.kwm0304.Models;

public class Transaction
{
  public int Id { get; set; }
  [StringLength(60, MinimumLength = 3)]
  [Required]
  public string? Title { get; set; }
  [Range(1, 100)]
  [DataType(DataType.Currency)]
  public decimal Amount { get; set; }
  [Display(Name = "Occured At")]
  [DataType(DataType.Date)]
  public DateTime OccuredAt { get; set; }
  [Required]
  public Category? TransactionCategory { get; set; }
  public int CategoryId { get; set; }
}
