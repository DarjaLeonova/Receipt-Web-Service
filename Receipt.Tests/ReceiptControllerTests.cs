using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Receipt_Api.Controllers;
using ReceiptApi.Core.Models;
using ReceiptApi.Core.Services;
using ReceiptApi.Tests.Utils;
using RentalApi.Services;

namespace ReceiptApi.Tests
{
    public class ReceiptControllerTests : ReceiptTestBase
    {
        private readonly ReceiptController _receiptController;
        private readonly ReceiptService _receiptService;
        private readonly IEntityService<Receipt> _entityService;
        private readonly DateTime _time = new DateTime(2022, 1, 1, 12, 0, 0);
        private readonly List<Item> _defaultItems;
        private readonly Receipt _defaultReceipt;

        public ReceiptControllerTests()
        {
            _receiptService = new ReceiptService(_context);
            _entityService = new EntityService<Receipt>(_context);
            _receiptController = new ReceiptController(_entityService, _receiptService);
            _defaultItems = new List<Item>();
            _defaultItems.Add(new Item { Id = 1, ProductName = "Banana", ReceiptId = 1 });
            _defaultItems.Add(new Item { Id = 2, ProductName = "Rice", ReceiptId = 1 });
            _defaultReceipt = new Receipt() { Id = 1, CreatedOn = _time, Items = _defaultItems };
        }

        [Fact]
        public void AddReceipt_ShouldReturnCreateResponse()
        {
            var createResponse = _receiptController.AddReceipt(_defaultReceipt);

            Assert.IsType<CreatedResult>(createResponse as CreatedResult);
        }

        [Fact]
        public void GetReceipt_ShouldReturnOkResponse()
        {
            _receiptController.AddReceipt(_defaultReceipt);
            var okResponse = _receiptController.GetReceipt(_defaultReceipt.Id);

            Assert.IsType<OkObjectResult>(okResponse as OkObjectResult);
        }

        [Fact]
        public void GetReceipt_ShouldReturnNotFoundResponse()
        {
            _receiptController.AddReceipt(_defaultReceipt);
            var okResponse = _receiptController.GetReceipt(500);

            Assert.IsType<NotFoundObjectResult>(okResponse as NotFoundObjectResult);
        }

        [Fact]
        public void GetReceipts_ReturnsOkResponse()
        {
            _receiptController.AddReceipt(_defaultReceipt);
            var okResponse = _receiptController.GetReceipts(null,null, "Banana");

            Assert.IsType<OkObjectResult>(okResponse as OkObjectResult);
        }

        [Fact]
        public void GetReceipts_ReturnsNotFoundResponse()
        {
            var notFoundResponse = _receiptController.GetReceipts(null, null, null);

            Assert.IsType<NotFoundObjectResult>(notFoundResponse as NotFoundObjectResult);
        }

        [Fact]
        public void DeleteReceipt_ReturnsOkResponse()
        {
            _receiptController.AddReceipt(_defaultReceipt);
            var okResponse = _receiptController.DeleteReceipt(_defaultReceipt.Id);

            Assert.IsType<OkObjectResult>(okResponse as OkObjectResult);
        }

        [Fact]
        public void DeleteReceipt_ReturnsNotFoundResponse()
        {
            _receiptController.AddReceipt(_defaultReceipt);
            var notFoundResponse = _receiptController.DeleteReceipt(5);

            Assert.IsType<NotFoundObjectResult>(notFoundResponse as NotFoundObjectResult);
        }

        [Fact]
        public void DeleteAllReceipts_ReturnsOkResponse()
        {
            _receiptController.AddReceipt(_defaultReceipt);
            var okResponse = _receiptController.DeleteAllReceipts();

            Assert.IsType<OkObjectResult>(okResponse as OkObjectResult);
        }
    }
}
