using AutoMapper;
using Business.Inventory.DTOs.CatalogueProduct;
using Business.Inventory.DTOs.Product;
using Business.Inventory.DTOs.ProductPrice;
using Services.Inventory.Models;

namespace Services.Inventory.Profiles
{
    public class InventoryMapperProfile : Profile
    {

        public InventoryMapperProfile()
        {
            // Product:
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>()
                .ForMember(dest => dest.Id, cfg => cfg.Ignore())
                .ForMember(dest => dest.Notes, cfg => cfg.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Notes = src.Notes == null ? dest.Notes : src.Notes ?? dest.Notes;
                })
                .ForMember(dest => dest.PhotoURL, cfg => cfg.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.PhotoURL = src.PhotoURL == null ? dest.PhotoURL : src.PhotoURL ?? dest.PhotoURL;
                })
                .ForAllMembers(cfg => cfg.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Product, ProductReadDTO>();
            CreateMap<Product, CatalogueProduct>()
                .ForMember(dest => dest.ProductId, cfg => cfg.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, cfg => cfg.Ignore());

            // CatalogueProduct:
            CreateMap<CatalogueProductCreateDTO, CatalogueProduct>()
                .ForMember(dest => dest.ProductPrice, cfg => cfg.MapFrom(src => src.ProductPrice));
            CreateMap<CatalogueProductUpdateDTO, CatalogueProduct>()
                .ForMember(dest => dest.ProductPrice, cfg => cfg.MapFrom(src => src.ProductPrice))
                .ForAllMembers(cfg => cfg.Condition((src, dest, srcMember) => srcMember != null));        
            CreateMap<CatalogueProduct, CatalogueProductReadDTO>()
                .ForMember(dest => dest.ProductPrice, cfg => cfg.MapFrom(src => src.ProductPrice));


            // ProductPrice:
            CreateMap<ProductPriceCreateDTO, ProductPrice>();
            CreateMap<ProductPriceUpdateDTO, ProductPrice>()                                                    
                .ForMember(dest => dest.SalePrice, cfg => cfg.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.SalePrice = src.SalePrice == 0 ? dest.SalePrice : src.SalePrice ?? dest.SalePrice;
                })
                .ForAllMembers(cfg => cfg.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProductPrice, ProductPriceReadDTO>();


        }

    }
}
