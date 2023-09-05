using Business.Data.Repositories.Interfaces;
using Services.Inventory.Models;

namespace Services.Inventory.Data.Repositories.Interfaces
{
    public interface IProductPriceRepository: IBaseRepository
    {
        Task<ProductPrice> GetProductPriceById(int id);
        Task<IEnumerable<ProductPrice>> GetProductPrices(IEnumerable<int> ProductIds);
        Task<bool> ProductExistsById(int id);
    }
}
