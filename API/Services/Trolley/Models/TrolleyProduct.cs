


namespace Services.Trolley.Models
{
    public record TrolleyProduct
    {
        public Guid TrolleyId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }


        public Trolley_model Trolley { get; set; }
    }
}
