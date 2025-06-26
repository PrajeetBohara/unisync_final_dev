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

        // Actions will go here...
    }
}
