


namespace Trolley.Models
{
    public class TrolleyPromotion
    {
        public bool IsOn { get; set; }
        public int TrolleyPromotionTypeId { get; set; }

        // All products in trolley if ProductId = 0.
        public int ProductId { get; set; }
        public int PriceLevel { get; set; }
        public int DiscountPercent { get; set; }



        public TrolleyPromotionType TrolleyPromotionType { get; set; }

    }
}
