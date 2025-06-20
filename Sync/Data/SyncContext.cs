using Microsoft.EntityFrameworkCore;
using Sync.Models;
using Sync.Data;

namespace Sync.Data
{
    public class SyncContext : DbContext
    {
        public SyncContext(DbContextOptions<SyncContext> options) : base(options) { }

        // DbSets representing tables in the database
        public DbSet<Reminder> Reminders { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
    }
}
