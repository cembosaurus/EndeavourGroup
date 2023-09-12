namespace Business.Inventory.DTOs.Product
{
    public record ProductReadDTO
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
        public string? PhotoURL { get; set; }
    }
}
