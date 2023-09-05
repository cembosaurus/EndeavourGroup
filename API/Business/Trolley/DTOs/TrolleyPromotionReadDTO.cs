

namespace Business.Trolley.DTOs
{
    public class TrolleyPromotionReadDTO
    {
        public bool IsOn { get; set; }
        public int TrolleyPromotionTypeId { get; set; }
        public int ProductId { get; set; }
        public int PriceLevel { get; set; }
        public int DiscountPercent { get; set; }
        public TrolleyPromotionTypeReadDTO TrolleyPromotionTypeReadDTO { get; set; }
    }

}
