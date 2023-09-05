using Business.Inventory.DTOs.Product;
using Business.Libraries.ServiceResult.Interfaces;

namespace Inventory.Services.Interfaces
{
    public interface IProductService
    {
        Task<IServiceResult<IEnumerable<ProductReadDTO>>> GetProducts(IEnumerable<int> productIds = null);
        Task<IServiceResult<ProductReadDTO>> GetProductById(int id);
        Task<IServiceResult<ProductReadDTO>> AddProduct(ProductCreateDTO product);
        Task<IServiceResult<ProductReadDTO>> UpdateProduct(int id, ProductUpdateDTO product);
        Task<IServiceResult<ProductReadDTO>> DeleteProduct(int id);
    }
}
