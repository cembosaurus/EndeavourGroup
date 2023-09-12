using Business.Inventory.DTOs.ProductPrice;
using FluentValidation;

namespace Business.Inventory.DTOs.CatalogueProduct
{
    public record CatalogueProductUpdateDTO
    {
        public string? Description { get; set; }
        public ProductPriceUpdateDTO? ProductPrice { get; set; }
        public int? Instock { get; set; }
    }


    public class CatalogueProductUpdateDTO_V : AbstractValidator<CatalogueProductUpdateDTO>
    {
        public CatalogueProductUpdateDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Catalogue Product update data model must NOT be NULL !");
            When(x => x != null, () => {
                RuleFor(x => x)
                    .Must(x => x.Description != null || x.ProductPrice != null || x.Instock != null)
                    .WithMessage("- At least one property is required in Catalogue Product update model !");

                When(x => !string.IsNullOrWhiteSpace(x.Description), () => {
                    RuleFor(x => x.Description)
                        .MaximumLength(100)
                        .WithMessage("- Description should NOT be longer than 100 chartacters !");
                });

                When(x => x.ProductPrice != null, () => {
                    RuleFor(x => x)
                        .Must(x => x.ProductPrice!.SalePrice.HasValue || x.ProductPrice.RRP.HasValue || x.ProductPrice.DiscountPercent.HasValue)
                        .WithMessage("- At least one property is required for Product Price update !");

                    When(x => x.ProductPrice!.SalePrice.HasValue, () => {
                        RuleFor(x => x.ProductPrice!.SalePrice)
                            .GreaterThan(0)
                            .WithMessage("- Sales Price should be HIGHER than '0' !");
                    });

                    When(x => x.ProductPrice!.RRP.HasValue, () => {
                        RuleFor(x => x.ProductPrice!.RRP)
                            .GreaterThan(0)
                            .WithMessage("- RRP should be HIGHER than '0' !");
                    });

                    When(x => x.ProductPrice!.DiscountPercent.HasValue, () => {
                        RuleFor(x => x.ProductPrice!.DiscountPercent)
                            .InclusiveBetween(0, 100)
                            .WithMessage("- Discount percent should be between '0' and '100' !");
                    });
                });

                When(x => x.Instock.HasValue, () => {
                    RuleFor(x => x.Instock)
                        .GreaterThanOrEqualTo(0)
                        .WithMessage("- Instock amount must be greater or equal to 0 !");
                });
            });
        }
    }
}
