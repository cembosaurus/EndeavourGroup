


namespace Business.Trolley.DTOs
{
    public class TrolleyProductReadDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double SalePrice { get; set; }
        public double DiscountedPrice { get; set; }
    }
}
