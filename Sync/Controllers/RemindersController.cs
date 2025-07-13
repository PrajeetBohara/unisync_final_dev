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



        // GET: /Reminders/Create
        public IActionResult Create()
        {
            // Prepare category list for dropdown in the view
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reminder);
                await _context.SaveChangesAsync();
                // On normal form submission, redirect back to list
                return RedirectToAction(nameof(Index));
            }
            // If validation failed, re-show form with current data and categories
            ViewBag.Categories = _context.Categories.ToList();
            return View(reminder);
        }



        // GET: /Reminders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null) return NotFound();
            // Load categories for dropdown
            ViewBag.Categories = _context.Categories.ToList();
            return View(reminder);
        }



        // POST: /Reminders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reminder reminder)
        {
            if (id != reminder.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reminder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Reminders.Any(e => e.Id == reminder.Id))
                {
                        return NotFound(); // It was deleted by someone else
                    }
                    throw; // Other concurrency issue – rethrow
                }
                return RedirectToAction(nameof(Index));
            }
            // Validation failed – reload categories and show form again
            ViewBag.Categories = _context.Categories.ToList();
            return View(reminder);
        }



        // POST: /Reminders/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();
            // If this was an AJAX request, we can return a JSON result:
            return Json(new { success = true });
        }







    }
}
