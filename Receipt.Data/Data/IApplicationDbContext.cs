using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReceiptApi.Core.Models;

namespace ReceiptApi.Data.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Receipt> Receipts { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
