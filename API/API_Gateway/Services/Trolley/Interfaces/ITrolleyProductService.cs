using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;



namespace API_Gateway.Services.Trolley.Interfaces
{
    public interface ITrolleyProductService
    {
        Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> AddProductsToTrolley(int trolleyId, IEnumerable<TrolleyProductUpdateDTO> productsToAdd);
        Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> DeleteTrolleyProducts(int userId, IEnumerable<int> productIds);
        Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> GetAllTrolleyProducts();
        Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> GetTrolleyProducts(int userId);
        Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> RemoveTrolleyProducts(int trolleyId, IEnumerable<TrolleyProductUpdateDTO> productsToRemove);
    }
}
