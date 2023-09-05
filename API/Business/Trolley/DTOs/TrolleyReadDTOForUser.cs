using Business.API_Gateway.DTOs.CatalogueProduct;



namespace Business.Trolley.DTOs
{
    public class TrolleyReadDTOForUser
    {
        public int UserId { get; set; }
        public double Total { get; set; }
        public IEnumerable<CatalogueProductReadDTO_View> CatalogueProducts { get; set; }
    }
}
