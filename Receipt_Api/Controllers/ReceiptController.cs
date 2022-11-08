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
            //var receipt = _entityService.GetById(id);
            var receipt = _receiptService.GetReceiptById(id);
            return Ok(receipt.Entity);
        }

        [Route("receipts")]
        [HttpGet]
        public IActionResult GetAllReceipts()
        {
            var receipts = _receiptService.GetAllReceipts();
            return Ok(receipts.Entity);
        }

        [Route("delete/receipt/{id}")]
        [HttpDelete]
        public IActionResult DeleteReceipt(int id)
        {
            var entity = _receiptService.GetReceiptById(id);
            //_entityService.Delete();
            return Ok(receipt.Entity);

        }
    }
}
