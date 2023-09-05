using Business.Inventory.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace Business.Inventory.Controllers.Interfaces
{
    public interface IProductController
    {
        Task<ActionResult> AddProduct(ProductCreateDTO product);
        Task<ActionResult> DeleteProductById(int id);
        Task<ActionResult> GetAllProducts();
        Task<ActionResult> GetProductById(int id);
        Task<ActionResult> GetProducts(IEnumerable<int> productIds);
        Task<ActionResult> UpdateProduct(int id, ProductUpdateDTO product);
    }
}
