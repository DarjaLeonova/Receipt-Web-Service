namespace ReceiptApi.Core.Models
{
    public class Item : Entity
    {
        public int ReceiptId { get; set; }
        public string ProductName { get; set; }
    }
}
