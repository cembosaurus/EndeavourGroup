﻿namespace Business.API_Gateway.DTOs.CatalogueProduct
{
    public class CatalogueProductReadDTO_View
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double SalesPrice { get; set; }
        public int Amount { get; set; }
    }
}
