


namespace Business.Trolley.DTOs
{
    public class TrolleyReadDTO
    {
        public Guid TrolleyId { get; set; }
        public int UserId { get; set; }
        public double Total { get; set; }
        public double DiscountedTotal
        {
            get
            {
                return TrolleyProducts.Sum(p => p.DiscountedPrice * p.Amount);
            }
        }

        public ICollection<TrolleyProductReadDTO> TrolleyProducts { get; set; }
    }
}
