using ReceiptApi.Core.Interfaces;
using ReceiptApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptApi.Core.Services
{
    public class ServiceResult
    {
        public bool Success { get; }
        public IEntity Entity { get; private set; }
        public IList<string> Errors { get; }
        public IList<Receipt> Receipts { get; set; }
        public string FormattedErrors => string.Join(", ", Errors);

        public ServiceResult(bool success)
        {
            Success = success;
            Errors = new List<string>();
            Receipts = new List<Receipt>();
        }

        public ServiceResult SetEntity(IEntity entity)
        {
            Entity = entity;
            return this;
        }

        public ServiceResult AddError(string error)
        {
            Errors.Add(error);
            return this;
        }
        public ServiceResult SetEntities(List<Receipt> receipts)
        {
            Receipts = receipts;
            return this;
        }

        public ServiceResult EmptyEntity()
        {
            Entity = null;
            return this;
        }
    }
}
