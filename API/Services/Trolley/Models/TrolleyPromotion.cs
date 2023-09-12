


namespace Trolley.Models
{
    public record TrolleyPromotion
    {
        public bool IsOn { get; set; }
        public int TrolleyPromotionTypeId { get; set; }
        public int ProductId { get; set; }
        public int SpendLevel { get; set; }
        public int DiscountPercent { get; set; }



        public TrolleyPromotionType TrolleyPromotionType { get; set; }

    }
}
