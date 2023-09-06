


namespace Services.Trolley.Models
{
    public class Trolley_model
    {
        public Guid TrolleyId { get; set; }
        public int UserId { get; set; }        
        public decimal Total { get; set; }

        public ICollection<TrolleyProduct> TrolleyProducts { get; set; }
    }
}
