

namespace Business.Trolley.DTOs
{
    public class TrolleyPromotionReadDTO
    {
        public int ProductId { get; set; }
        public int PriceLevel { get; set; }
        public int DiscountPercent { get; set; }
        public TrolleyPromotionTypeReadDTO TrolleyPromotionType { get; set; }
    }

}
