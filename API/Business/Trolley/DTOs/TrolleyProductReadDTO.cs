


namespace Business.Trolley.DTOs
{
    public class TrolleyProductReadDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }

        public int Amount { get; set; }
        public decimal ProductTotal { get; set; }
        public decimal ProductDiscountedPrice { get; set; }
        public decimal Saved
        {
            get
            {
                return SalePrice - ProductDiscountedPrice;
            }
        }

    }
}
