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
            return new ServiceResult(true).SetEntity(entity);
        }

        public ServiceResult DeleteById(int id)
        {
            var entity = GetReceiptById(id);
            base.Delete(entity.Entity);
            _context.Receipts.Remove(entity.Entity);
            _context.SaveChanges();
            return new ServiceResult(true).SetEntity(entity);
        }
    }
}
