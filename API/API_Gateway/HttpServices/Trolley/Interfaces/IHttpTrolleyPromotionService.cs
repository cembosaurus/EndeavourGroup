

using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;

namespace API_Gateway.HttpServices.Trolley.Interfaces
{
    public interface IHttpTrolleyPromotionService
    {
        Task<IServiceResult<IEnumerable<TrolleyPromotionReadDTO>>> GetActiveTrolleyPromotions();
        Task<IServiceResult<IEnumerable<TrolleyPromotionReadDTO>>> GetAllTrolleyPromotions();
    }
}
