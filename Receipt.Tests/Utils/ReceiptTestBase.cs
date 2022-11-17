using Microsoft.EntityFrameworkCore;
using ReceiptApi.Data.Data;

namespace ReceiptApi.Tests.Utils
{
    public class ReceiptTestBase : IDisposable
    {
        protected readonly ApplicationDbContext _context;

        public ReceiptTestBase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
