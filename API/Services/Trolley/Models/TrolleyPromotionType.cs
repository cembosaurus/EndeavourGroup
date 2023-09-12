namespace Trolley.Models
{
    public record TrolleyPromotionType
    {
        public int TrolleyPromotionTypeId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }



        public TrolleyPromotion TrolleyPromotion { get; set; }


        // Promotion Types in appsettings: BOGOF = 1, BOGSOFHP = 2, SAS = 3
    }
}
