using Microsoft.AspNetCore.Mvc;
using Sync.Data;
using Sync.Models;
using Microsoft.EntityFrameworkCore;

namespace Sync.Controllers
{
    public class RemindersController : Controller
    {
        private readonly SyncContext _context;
        public RemindersController(SyncContext context)
        {
            _context = context;
        }

        // GET: /Reminders or /Reminders?categoryId=#
        public async Task<IActionResult> Index(int? categoryId)
        {
            // Load categories for dropdown filter (pass to ViewBag)
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = categoryId; // to remember current filter selection in the view
            // Query reminders, including category data
            IQueryable<Reminder> query = _context.Reminders.Include(r => r.Category);
            if (categoryId.HasValue && categoryId.Value != 0)
            {
                query = query.Where(r => r.CategoryId == categoryId.Value);
            }
            var remindersList = await query.ToListAsync();
            return View(remindersList);
        }

    }
}
