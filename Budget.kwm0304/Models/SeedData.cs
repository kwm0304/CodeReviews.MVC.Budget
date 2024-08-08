using Microsoft.EntityFrameworkCore;

namespace Budget.kwm0304.Models;

public class SeedData
{
  public static void InitializeCategories(IServiceProvider serviceProvider)
  {
    using var context = new BudgetContext(serviceProvider.GetRequiredService<DbContextOptions<BudgetContext>>());
    if (context.Categories.Any())
    {
      return;
    }
    context.Categories.AddRange(
      new Category
      {
        Name = "Utilities"
      },
      new Category
      {
        Name = "Food"
      },
      new Category
      {
        Name = "Insurance"
      },
      new Category
      {
        Name = "Transportation"
      },
      new Category
      {
        Name = "Health Care"
      },
      new Category
      {
        Name = "Housing"
      },
      new Category
      {
        Name = "Entertainment"
      },
      new Category
      {
        Name = "Rent"
      },
      new Category
      {
        Name = "Business Fees"
      },
      new Category
      {
        Name = "Childcare"
      },
      new Category
      {
        Name = "Clothing"
      },
      new Category
      {
        Name = "Saving"
      },
      new Category
      {
        Name = "Gym"
      },
      new Category
      {
        Name = "Home Insurance"
      },
      new Category
      {
        Name = "Salary"
      },
      new Category
      {
        Name = "Travel Expenses"
      },
      new Category
      {
        Name = "Gas"
      }
    );
    context.SaveChanges();
  }
}