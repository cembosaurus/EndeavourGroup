using API_Gateway.HttpServices.Trolley.Interfaces;
using API_Gateway.Services.Trolley.Interfaces;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;



namespace API_Gateway.Services.Trolley
{
    public class TrolleyProductService : ITrolleyProductService
    {

        private readonly IHttpTrolleyProductService _httpTrolleyProductService;


        public TrolleyProductService(IHttpTrolleyProductService httpTrolleyProductService)
        {
            _httpTrolleyProductService = httpTrolleyProductService;
        }





        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> GetAllTrolleyProducts()
        {
            return await _httpTrolleyProductService.GetAllTrolleyProducts();
        }



        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> GetTrolleyProducts(int userId)
        {
            return await _httpTrolleyProductService.GetTrolleyProducts(userId);
        }



        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> AddProductsToTrolley(int userId, IEnumerable<TrolleyProductUpdateDTO> productsToAdd)
        {
            return await _httpTrolleyProductService.AddProductsToTrolley(userId, productsToAdd);
        }



        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> RemoveTrolleyProducts(int userId, IEnumerable<TrolleyProductUpdateDTO> productsToRemove)
        {
            return await _httpTrolleyProductService.RemoveTrolleyProducts(userId, productsToRemove);
        }

        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> DeleteTrolleyProducts(int userId, IEnumerable<int> productIds)
        {
            return await _httpTrolleyProductService.DeleteTrolleyProducts(userId, productIds);
        }
    }
}
