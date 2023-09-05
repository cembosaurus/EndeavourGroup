

using Business.Trolley.DTOs;

namespace Business.Trolley.Http.Interfaces
{
    public interface IHttpTrolleyProductClient
    {
        Task<HttpResponseMessage> AddProductsToTrolley(int trolleyId, IEnumerable<TrolleyProductUpdateDTO> productsToAdd);
        Task<HttpResponseMessage> DeleteTrolleyProducts(int userId, IEnumerable<int> productIds);
        Task<HttpResponseMessage> GetAllTrolleyProducts();
        Task<HttpResponseMessage> GetTrolleyProducts(int userId);
        Task<HttpResponseMessage> RemoveTrolleyProducts(int trolleyId, IEnumerable<TrolleyProductUpdateDTO> productsToRemove);
    }
}
