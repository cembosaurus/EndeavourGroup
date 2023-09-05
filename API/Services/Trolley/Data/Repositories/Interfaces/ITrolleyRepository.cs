using Business.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.Trolley.Models;



namespace Trolley.Data.Repositories.Interfaces
{
    public interface ITrolleyRepository : IBaseRepository
    {
        Task<EntityEntry> CreateTrolley(Trolley_model trolley);
        Task<EntityEntry> DeleteTrolley(Trolley_model trolley);
        Task<bool> ExistsByTrolleyId(Guid id);
        Task<bool> ExistsByUserId(int id);
        Task<Trolley_model> GetTrolleyByTrolleyId(Guid trolleyId);
        Task<Trolley_model> GetTrolleyByUserId(int userId);
        Task<Guid> GetTrolleyIdByUserId(int userId);
        Task<IEnumerable<Trolley_model>> GetTrolleys();
        Task<int> GetUserIdByTrolleyId(Guid trolleyId);
    }
}
