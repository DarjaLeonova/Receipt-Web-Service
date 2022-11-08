using Microsoft.EntityFrameworkCore;
using ReceiptApi.Core.Models;
using ReceiptApi.Core.Services;
using ReceiptApi.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApi.Services
{
    public class ReceiptService : EntityService<Receipt>, IReceiptService
    {
        public ReceiptService(ApplicationDbContext context) : base(context)
        {
            
        }
        
        public ServiceResult GetReceiptById(int id)
        {
            var entity = _context.Receipts
                .Include(x => x.Items)
                .SingleOrDefault(x => x.Id == id);
            return new ServiceResult(true).SetEntity(entity);
        }

        public ServiceResult GetAllReceipts()
        {
            var entity = _context.Receipts
                .Include(x => x.Items).ToList();
            return new ServiceResult(true).SetEntities(entity); 
            
        }

        public ServiceResult DeleteById(int id)
        {
            var receiptToRemove = _context.Items.Where(e => e.ReceiptId == id);
            _context.Items.RemoveRange(receiptToRemove);
            _context.Receipts.Remove((Receipt)GetReceiptById(id).Entity);
            _context.SaveChanges();
            return new ServiceResult(true).EmptyEntity();
        }

        public ServiceResult DeleteAll()
        {
            var entity = _context.Receipts
                .Include(x => x.Items);
            _context.Receipts.RemoveRange(entity);
           // _context.Items.RemoveRange();
            _context.SaveChanges();
            return new ServiceResult(true).EmptyEntity();
        }
    }
}
