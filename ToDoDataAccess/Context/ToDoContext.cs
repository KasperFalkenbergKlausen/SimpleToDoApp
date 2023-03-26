using Microsoft.EntityFrameworkCore;
using ToDoDataAccess.Models;

namespace ToDoDataAccess.Context
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Item> Items { get; set; }

        public override int SaveChanges()
        {
            var time = DateTime.Now;

            var changedEntries = ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged);

            foreach (var entry in changedEntries)
            {
                if (entry.Entity is Item && entry.State is EntityState.Added)
                {
                    entry.Property("CreatedDate").CurrentValue = entry.Property("UpdatedDate").CurrentValue = time;
                    entry.Property("Status").CurrentValue = Status.InProgress;
                }
                else if (entry.Entity is Item && entry.State is EntityState.Modified)
                    entry.Property("UpdatedDate").CurrentValue = time;
            }

            return base.SaveChanges();
        }
    }
}
