using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Budget.StevieTV.Database;
using Budget.StevieTV.Models;
using Humanizer;

namespace Budget.StevieTV.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BudgetContext _context;

        public CategoryController(BudgetContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var transactions = await _context.Transactions.Include(t => t.Category).OrderBy(t => t.Date).ToListAsync();
            var categories = await _context.Categories.ToListAsync();

            var viewModel = new BudgetViewModel
            {
                Transactions = transactions,
                Categories = categories,
                TransactionViewModel = new TransactionViewModel(categories),
                CategoryViewModel = new CategoryViewModel()
            };

            return View(viewModel);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Json(category);
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BudgetViewModel budgetViewModel)
        {

            var newCategory = new Category
            {
                Id = budgetViewModel.CategoryViewModel.Id,
                Name = budgetViewModel.CategoryViewModel.Name,
            };

            if (newCategory.Id > 0)
            {
                _context.Update(newCategory);
            }
            else
            {
                _context.Add(newCategory);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> DuplicateCategoryName([Bind(Prefix = "CategoryViewModel.Name")] string name)
        {
            var categories = await _context.Categories.ToListAsync();

            if (categories.Any(c => c.Name.ToLower() == name.ToLower()))
                return Json("Duplicate Category");

            return Json(true);

        }
    }
}