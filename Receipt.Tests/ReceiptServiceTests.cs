using ReceiptApi.Tests.Utils;
using RentalApi.Services;
using ReceiptApi.Core.Models;
using FluentAssertions;

namespace ReceiptApi.Tests
{
    public class ReceiptServiceTests : ReceiptTestBase
    {
        private readonly ReceiptService _receiptService;
        private readonly DateTime _time = new DateTime(2022, 1, 1, 12, 0, 0);
        private readonly List<Item> _defaultItems;
        private readonly Receipt _defaultReceipt;

        public ReceiptServiceTests()
        {
            _receiptService = new ReceiptService(_context);
            _defaultItems = new List<Item>();
            _defaultItems.Add(new Item { Id = 1, ProductName = "Banana", ReceiptId = 1 });
            _defaultItems.Add(new Item { Id = 2, ProductName = "Rice", ReceiptId = 1 });
            _defaultReceipt = new Receipt() { Id = 1, CreatedOn = _time, Items = _defaultItems };
        }

        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await _context.Database.CanConnectAsync());
        }

        [Fact]
        public void AddReceipt_ShouldReturnReceipt()
        {
            var act = _receiptService.Create(_defaultReceipt).Entity;
            act.Should().Be(_defaultReceipt);
        }

        [Fact]
        public void GetReceiptById_ShouldReturnReceipt()
        {
            _context.Add(_defaultReceipt);
            _context.SaveChanges();

            var act = _receiptService.GetReceiptById(_defaultReceipt.Id).Entity;
            act.Should().Be(_defaultReceipt);
        }

        [Fact]
        public void GetAllReceipts_ShouldReturnAllReceipts()
        {
            _context.Add(_defaultReceipt);
            _context.SaveChanges();

            var act = _receiptService.GetAllReceipts().Receipts;
            act.Count.Should().Be(1);
        }

        [Fact]
        public void DeleteReceiptById_ShouldReturnNull()
        {
            _context.Add(_defaultReceipt);
            _context.SaveChanges();

            var act = _receiptService.DeleteById(_defaultReceipt.Id).Entity;
            act.Should().Be(_defaultReceipt);
        }

        [Fact]
        public void DeleteAllReceipts_ShouldReturnZero()
        {
            _context.Add(_defaultReceipt);
            _context.SaveChanges();

            var act = _receiptService.DeleteAll().Receipts;
            act.Count.Should().Be(0);
        }

        [Fact]
        public void FindSpecificReceipts_AllAttributesAreNull_ShouldReturnAllReceipts()
        {
            _context.Add(_defaultReceipt);
            _context.SaveChanges();

            var act = _receiptService.FindReceipts(null, null, null).Receipts;
            act.Count.Should().Be(1);
        }

        [Fact]
        public void FindSpecificReceipts_NameAttributeIsNotNull_ShouldReturnReceipt()
        {
            _context.Add(_defaultReceipt);
            _context.SaveChanges();

            var act = _receiptService.FindReceipts(null, null, "Banana").Receipts;
            act.Count.Should().Be(1);
        }

        [Fact]
        public void FindSpecificReceipts_StartDateAttributeIsNotNull_ShouldReturnReceipt()
        {
            _context.Add(_defaultReceipt);
            _context.SaveChanges();

            var act = _receiptService.FindReceipts(_defaultReceipt.CreatedOn, null, null).Receipts;
            act.Count.Should().Be(1);
        }

        [Fact]
        public void FindSpecificReceipts_EndDateAttributeIsNotNull_ShouldReturnReceipt()
        {
            _context.Add(_defaultReceipt);
            _context.SaveChanges();

            var act = _receiptService.FindReceipts(null, _defaultReceipt.CreatedOn, null).Receipts;
            act.Count.Should().Be(1);
        }

        [Fact]
        public void FindSpecificReceipts_AllAttributesAreNotNull_ShouldReturnReceipt()
        {
            _context.Add(_defaultReceipt);
            _context.SaveChanges();

            var act = _receiptService.FindReceipts(_defaultReceipt.CreatedOn, _defaultReceipt.CreatedOn, "Banana").Receipts;
            act.Count.Should().Be(1);
        }
    }
}