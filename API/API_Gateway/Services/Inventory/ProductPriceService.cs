using API_Gateway.HttpServices.Inventory.Interfaces;
using API_Gateway.Services.Inventory.Interfaces;
using Business.Inventory.DTOs.ProductPrice;
using Business.Libraries.ServiceResult.Interfaces;



namespace API_Gateway.Services.Inventory
{
    public class ProductPriceService : IProductPriceService
    {

        private readonly IHttpProductPriceService _httpProductPriceService;


        public ProductPriceService(IHttpProductPriceService httpProductPriceService)
        {
            _httpProductPriceService = httpProductPriceService;
        }





        public async Task<IServiceResult<ProductPriceReadDTO>> GetProductPriceById(int id)
        {
            return await _httpProductPriceService.GetProductPriceById(id);
        }



        public async Task<IServiceResult<IEnumerable<ProductPriceReadDTO>>> GetProductPrices(IEnumerable<int> productIds = default)
        {
            return await _httpProductPriceService.GetProductPrices(productIds);
        }



        public async Task<IServiceResult<ProductPriceReadDTO>> UpdateProductPrice(int productId, ProductPriceUpdateDTO productPriceEditDTO)
        {
            return await _httpProductPriceService.UpdateProductPrice(productId, productPriceEditDTO);
        }
    }
}
