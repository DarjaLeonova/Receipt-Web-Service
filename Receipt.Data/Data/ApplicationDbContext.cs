using Microsoft.EntityFrameworkCore;
using ReceiptApi.Core.Models;

namespace ReceiptApi.Data.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Item> Items { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
