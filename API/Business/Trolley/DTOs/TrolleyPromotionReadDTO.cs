

namespace Business.Trolley.DTOs
{
    public record TrolleyPromotionReadDTO
    {
        public int ProductId { get; set; }
        public int SpendLevel { get; set; }
        public int DiscountPercent { get; set; }
        public TrolleyPromotionTypeReadDTO TrolleyPromotionType { get; set; }
    }

}
