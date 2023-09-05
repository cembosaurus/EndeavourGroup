using Business.Data.Repositories.Interfaces;
using Trolley.Models;

namespace Trolley.Data.Repositories.Interfaces
{
    public interface ITrolleyPromotionsRepository : IBaseRepository
    {
        Task<IEnumerable<TrolleyPromotion>> GetActiveTrolleyPromotions();
        Task<IEnumerable<TrolleyPromotion>> GetAllTrolleyPromotions();
    }
}
