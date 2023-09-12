

namespace Business.Trolley.DTOs
{
    public record TrolleyPromotionTypeReadDTO
    {
        public int TrolleyPromotionTypeId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }

}
