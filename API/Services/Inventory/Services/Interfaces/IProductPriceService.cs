using Business.Inventory.DTOs.ProductPrice;
using Business.Libraries.ServiceResult.Interfaces;

namespace Inventory.Services.Interfaces
{
    public interface IProductPriceService
    {
        Task<IServiceResult<ProductPriceReadDTO>> GetProductPriceById(int id);
        Task<IServiceResult<IEnumerable<ProductPriceReadDTO>>> GetProductPrices(IEnumerable<int> productIds = null);
        Task<IServiceResult<ProductPriceReadDTO>> UpdateProductPrice(int productId, ProductPriceUpdateDTO productPriceEditDTO);
    }
}
