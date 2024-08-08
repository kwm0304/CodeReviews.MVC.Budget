using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.kwm0304.Models;

public class TransactionViewModel
{
  public List<Transaction>? Transactions { get; set; }
  public SelectList? Categories { get; set; }
  public string? TransactionCategory { get; set; }
  public string? SearchString { get; set; }
}
