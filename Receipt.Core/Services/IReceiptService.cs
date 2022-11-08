using ReceiptApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptApi.Core.Services
{
    public interface IReceiptService
    {
        ServiceResult GetReceiptById(int id);
        ServiceResult GetAllReceipts();
        ServiceResult DeleteById(int id);
    }
}
