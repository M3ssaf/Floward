using Common.Models.Responses;

namespace QueueService.BusinessContract
{
    public interface IProductsNotification
    {
        Task<ProductResponse> GetProduct(long Id); 
    }
}
