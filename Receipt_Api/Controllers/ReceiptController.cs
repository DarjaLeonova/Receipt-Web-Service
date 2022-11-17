using Microsoft.AspNetCore.Mvc;
using ReceiptApi.Core.Models;
using ReceiptApi.Core.Services;

namespace Receipt_Api.Controllers
{
    [Route("receipt-api")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IEntityService<Receipt> _entityService;
        private readonly IReceiptService _receiptService;

        public ReceiptController(IEntityService<Receipt> entityService, IReceiptService receiptService)
        {
            _entityService = entityService;
            _receiptService = receiptService;
        }

        [Route("add/receipt")]
        [HttpPut]
        public IActionResult AddReceipt(Receipt receipt)
        {
            _entityService.Create(receipt);

            return Created("", receipt);
        }

        [Route("receipt/{id}")]
        [HttpGet]
        public IActionResult GetReceipt(int id)
        {
            var serviceResult = _receiptService.GetReceiptById(id);

            return Result(serviceResult);
        }

        [Route("receipts")]
        [HttpGet]
        public IActionResult GetReceipts(DateTime? startDate, DateTime? endRange, string? productName)
        {
            var serviceResult = _receiptService.FindReceipts(startDate, endRange, productName);

            return Result(serviceResult);
        }

        [Route("delete/receipt/{id}")]
        [HttpDelete]
        public IActionResult DeleteReceipt(int id)
        {
            var serviceResult = _receiptService.DeleteById(id);

            return Result(serviceResult);
        }

        [Route("delete/receipts")]
        [HttpDelete]
        public IActionResult DeleteAllReceipts()
        {
            var serviceResult = _receiptService.DeleteAll();

            return Result(serviceResult);
        }

        private IActionResult Result(ServiceResult serviceResult)
        {
            if (!serviceResult.Success) 
                return NotFound(serviceResult.FormattedErrors);
            if (serviceResult.Success && serviceResult.Receipts.Count > 0) 
                return Ok(serviceResult.Receipts.ToList());
            else
                return Ok(serviceResult.Entity);
        }
    }
}
