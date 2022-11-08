using ReceiptApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptApi.Core.Services
{
    public class ServiceResult
    {
        public ServiceResult(bool success)
        {
            Success = success;
            Errors = new List<string>();
        }

        public bool Success { get; }
        public IEntity Entity { get; private set; }

        public IList<string> Errors { get; }

        public string FormattedErrors => string.Join(", ", Errors);

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
    }

}
