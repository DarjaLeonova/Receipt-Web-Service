using Microsoft.EntityFrameworkCore;
using ReceiptApi.Core.Models;
using ReceiptApi.Core.Services;
using ReceiptApi.Data.Data;

namespace RentalApi.Services
{
    public class ReceiptService : EntityService<Receipt>, IReceiptService
    {
        public ReceiptService(ApplicationDbContext context) : base(context) { }
        
        public ServiceResult GetReceiptById(int id)
        {
            var entity = _context.Receipts
                .Include(x => x.Items)
                .SingleOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return new ServiceResult(false).AddError($"Entity With {id} does not exists");
            }

            return new ServiceResult(true).SetEntity(entity);
        }

        public ServiceResult GetAllReceipts()
        {
            var entities = _context.Receipts
                .Include(x => x.Items).ToList();

            return new ServiceResult(true).SetEntities(entities);  
        }

        public ServiceResult DeleteById(int id)
        {
            var entity = GetReceiptById(id).Entity;

            if (entity == null) 
            { 
                return new ServiceResult(false).AddError($"Entity With {id} does not exists"); 
            }

            var receiptToRemove = _context.Items.Where(e => e.ReceiptId == id);

            _context.Items.RemoveRange(receiptToRemove);
            _context.Receipts.Remove((Receipt)entity);
            _context.SaveChanges();

            return new ServiceResult(true).SetEntity(entity);
        }

        public ServiceResult DeleteAll()
        {
            var entity = _context.Receipts
                .Include(x => x.Items);

            if (entity.ToList().Count == 0)
            {
                return new ServiceResult(false).AddError("No entites to delete");
            }

            _context.Receipts.RemoveRange(entity);
            _context.SaveChanges();

            return new ServiceResult(true).EmptyEntity();
        }

        public ServiceResult FindReceipts(DateTime? startDate, DateTime? endDate, string? productName)
        {
            var startDateOrDefault = startDate ?? DateTime.MinValue;
            var endDateOrDefault = endDate ?? DateTime.MaxValue;
            var productNameOrDefault = productName ?? "";

            var receipts = _context.Receipts.Include(x => x.Items);
            var query = new List<Receipt>();

            if (receipts.ToList().Count == 0)
            {
                return new ServiceResult(false).AddError("No receipts in database");
            }

            if (productNameOrDefault == "")
            {
                query = receipts
                    .Where(receipt => receipt.CreatedOn >= startDateOrDefault && receipt.CreatedOn <= endDateOrDefault).ToList();
            } 
            else 
            {
                query = receipts
                    .Where(receipt => receipt.CreatedOn >= startDateOrDefault
                    && receipt.CreatedOn <= endDateOrDefault
                    && receipt.Items
                    .Any(item => item.ProductName == productNameOrDefault))
                    .ToList();
            }
                
            return new ServiceResult(true).SetEntities(query);
        }
    }
}
