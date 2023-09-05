using Business.Data.Repositories.Interfaces;
using Services.Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Inventory.Data.Repositories.Interfaces
{
    public interface IProductRepository: IBaseRepository
    {
        Task<IEnumerable<Product>> GetProducts(IEnumerable<int> ProductIds = null);
        Task<Product> GetProductById(int id);
        Task<Product> GetProductByName(string name);
        Task<EntityState> AddProduct(Product Product);
        Task<EntityState> DeleteProduct(Product Product);
        Task<bool> ExistsById(int id);
        Task<bool> ExistsByName(string name);
    }
}
