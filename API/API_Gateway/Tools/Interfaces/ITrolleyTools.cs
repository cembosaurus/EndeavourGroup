using Business.Trolley.DTOs;

namespace API_Gateway.Tools.Interfaces
{
    public interface ITrolleyTools
    {
        void ApplyTrolleyPromotionDiscount(TrolleyReadDTO trolley, IEnumerable<TrolleyPromotionReadDTO> activePromotions);
    }
}
