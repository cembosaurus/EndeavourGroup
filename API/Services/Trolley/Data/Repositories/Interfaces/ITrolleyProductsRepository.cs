using Business.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.Trolley.Models;



namespace Trolley.Data.Repositories.Interfaces
{
    public interface ITrolleyProductsRepository : IBaseRepository
    {
        Task<EntityEntry> DeleteTrolleyProduct(TrolleyProduct trolleyProduct);
        Task DeleteTrolleyProducts(IEnumerable<TrolleyProduct> trolleyProducts);
        Task<IEnumerable<TrolleyProduct>> GetTrolleyProductsByTrolleyId(Guid trolleyId);
        Task<IEnumerable<TrolleyProduct>> GetTrolleyProductsByUserId(int userId);
        Task<IEnumerable<TrolleyProduct>> GetTrolleyProducts(Guid trolleyId);
    }
}
