using ReceiptApi.Tests.Utils;
using ReceiptApi.Core.Services;
using RentalApi.Services;
using ReceiptApi.Core.Models;
using FluentAssertions;

namespace ReceiptApi.Tests
{
    public class UnitTest1 : ReceiptTestBase
    {
        private readonly ReceiptService _receiptService;
        private readonly DateTime _time = new DateTime(2022, 1, 1, 12, 0, 0);
        public UnitTest1()
        {
            _receiptService = new ReceiptService(_context);
        }

        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await _context.Database.CanConnectAsync());
        }

        [Fact]
        public void GetReceiptById()

        {
            var list = new List<Item>();
            list.Add(new Item { Id = 1, ProductName = "Kaka", ReceiptId = 1 });
            var receipt = new Receipt() { Id = 1, CreatedOn = _time, Items = list };

            _context.Add(receipt);
            _context.SaveChanges();

            var act = _receiptService.GetReceiptById(receipt.Id).Entity;
            act.Should().Be(receipt);
        }
    }
}