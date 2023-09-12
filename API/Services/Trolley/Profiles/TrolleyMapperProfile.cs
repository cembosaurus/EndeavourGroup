using AutoMapper;
using Business.API_Gateway.DTOs.CatalogueProduct;
using Business.Inventory.DTOs.CatalogueProduct;
using Business.Inventory.DTOs.Product;
using Business.Inventory.DTOs.ProductPrice;
using Business.Trolley.DTOs;
using Services.Trolley.Models;
using Trolley.Models;

namespace Trolley.Profiles
{
    public class TrolleyMapperProfile : Profile
    {

        public TrolleyMapperProfile()
        {
            // Trolley:

            CreateMap<Trolley_model, TrolleyReadDTO>();
            CreateMap<TrolleyProductUpdateDTO, TrolleyProduct>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Trolley_model, TrolleyReadDTOForUser>();


            // Trolley-Products:

            CreateMap<TrolleyProduct, TrolleyProductReadDTO>();
            CreateMap<ProductReadDTO, TrolleyProductReadDTO>();
            CreateMap<ProductPriceReadDTO, TrolleyProductReadDTO>();
            CreateMap<CatalogueProductReadDTO, CatalogueProductReadDTO_View>()
                .ForMember(dest => dest.SalesPrice, opt => opt.MapFrom(src => src.ProductPrice.SalePrice))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name));


            // Trolley-Promotions:

            CreateMap<TrolleyPromotion, TrolleyPromotionReadDTO>()
                .ForMember(dest => dest.TrolleyPromotionType, opt => opt.MapFrom(src => src.TrolleyPromotionType));
            CreateMap<TrolleyPromotionCreateDTO, TrolleyPromotion>();
            CreateMap<TrolleyPromotionType, TrolleyPromotionTypeReadDTO>();
            CreateMap<TrolleyPromotionTypeCreateDTO, TrolleyPromotionType>();

        }

    }
}
