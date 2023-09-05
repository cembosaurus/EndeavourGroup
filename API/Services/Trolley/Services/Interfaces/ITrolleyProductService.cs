using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;



namespace Trolley.Services.Interfaces
{
    public interface ITrolleyProductService
    {
        Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> AddProductsToTrolley(int trolleyId, IEnumerable<TrolleyProductUpdateDTO> productsToAdd);
        Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> DeleteProductsFromTrolley(int userId, IEnumerable<int> productIds);
        Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> GetTrolleyProducts(int userId);
        Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> RemoveProductsFromTrolley(int trolleyId, IEnumerable<TrolleyProductUpdateDTO> productsToRemove);
    }
}
