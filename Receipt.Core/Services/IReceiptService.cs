namespace ReceiptApi.Core.Services
{
    public interface IReceiptService
    {
        ServiceResult GetReceiptById(int id);
        ServiceResult GetAllReceipts();
        ServiceResult DeleteById(int id);
        ServiceResult DeleteAll();
        ServiceResult FindReceipts(DateTime? startDate, DateTime? endDate, string? productName);
    }
}
