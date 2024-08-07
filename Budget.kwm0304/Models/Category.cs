using System.ComponentModel.DataAnnotations;

namespace Budget.kwm0304.Models;

public class Category
{
  public int Id { get; set; }
  [StringLength(60, MinimumLength = 3)]
  [Required]
  public string? Name { get; set; }
  public List<Transaction>? Transactions { get; set; }
}
