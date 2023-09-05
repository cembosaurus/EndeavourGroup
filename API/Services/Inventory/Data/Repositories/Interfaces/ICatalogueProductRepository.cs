using Business.Data.Repositories.Interfaces;
using Services.Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Inventory.Data.Repositories.Interfaces
{
    public interface ICatalogueProductRepository: IBaseRepository
    {
        Task<EntityState> CreateCatalogueProduct(CatalogueProduct catalogueProduct);
        Task<bool> ExistsById(int id);
        Task<bool> ExistsByName(string name);
        Task<CatalogueProduct> GetCatalogueProductById(int id);
        Task<IEnumerable<CatalogueProduct>> GetCatalogueProducts(IEnumerable<int> ProductIds = null);
        Task<CatalogueProduct> GetCatalogueProductWithExtrasById(int id);
        Task<int> GetInstockCount(int id);
        Task<Product> GetProductById(int id);
        Task<Product> GetProductByName(string name);
        Task<EntityState> RemoveCatalogueProduct(CatalogueProduct catalogueProduct);
    }
}
