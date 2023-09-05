using Business.Inventory.DTOs.ProductPrice;
using Business.Libraries.ServiceResult.Interfaces;



namespace API_Gateway.HttpServices.Inventory.Interfaces
{
    public interface IHttpProductPriceService
    {
        Task<IServiceResult<ProductPriceReadDTO>> GetProductPriceById(int id);
        Task<IServiceResult<IEnumerable<ProductPriceReadDTO>>> GetProductPrices(IEnumerable<int> productIds = null);
        Task<IServiceResult<ProductPriceReadDTO>> UpdateProductPrice(int productId, ProductPriceUpdateDTO productPriceEditDTO);
    }
}
