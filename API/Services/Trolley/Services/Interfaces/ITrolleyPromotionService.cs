using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;

namespace Trolley.Services.Interfaces
{
    public interface ITrolleyPromotionService
    {
        Task<IServiceResult<IEnumerable<TrolleyPromotionReadDTO>>> GetActiveTrolleyPromotions();
        Task<IServiceResult<IEnumerable<TrolleyPromotionReadDTO>>> GetAllTrolleyPromotions();
    }
}
