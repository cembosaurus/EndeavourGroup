


namespace Business.Trolley.DTOs
{
    public class TrolleyReadDTO
    {
        public Guid TrolleyId { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountedTotal
        {
            get
            {
                return TrolleyProducts.Sum(p => p.ProductDiscountedPrice * p.Amount);
            }
        }
        public decimal SavedTotal
        {
            get
            {
                return Total - DiscountedTotal;
            }
        }

        public ICollection<TrolleyProductReadDTO> TrolleyProducts { get; set; }
    }
}
