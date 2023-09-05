using Business.Inventory.DTOs.ProductPrice;
using FluentValidation;

namespace Business.Inventory.DTOs.CatalogueProduct
{
    public class CatalogueProductCreateDTO
    {
        public string? Description { get; set; }
        public ProductPriceCreateDTO ProductPrice { get; set; }
        public int? Instock { get; set; }
    }


    public class CatalogueProductCreateDTO_V : AbstractValidator<CatalogueProductCreateDTO>
    {
        public CatalogueProductCreateDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Catalogue Product create model must NOT be NULL !");
            When(x => x != null, () => {
                When(x => !string.IsNullOrWhiteSpace(x.Description), () => {
                    RuleFor(x => x.Description)
                    .MaximumLength(100)
                    .WithMessage("- Description should NOT be longer than 100 characters !");
                });

                RuleFor(x => x.ProductPrice)
                    .NotNull()
                    .WithMessage("- Product Price update model must NOT be NULL !");
                When(x => x.ProductPrice != null, () =>
                {
                    RuleFor(x => x.ProductPrice.SalePrice)
                        .GreaterThan(0)
                        .WithMessage("- SalePrice must be greater than 0 !");
                    When(x => x.ProductPrice.RRP.HasValue, () => {
                        RuleFor(x => x.ProductPrice.RRP)
                        .GreaterThan(0)
                        .WithMessage("- RRP must be greater than 0 !");
                    });
                    When(x => x.ProductPrice.DiscountPercent.HasValue, () => {
                        RuleFor(x => x.ProductPrice.DiscountPercent)
                        .InclusiveBetween(1, 100)
                        .WithMessage("- Discount Percent must be in range 1 - 100 !");
                    });
                });

                When(x => x.Instock != null, () => {
                    RuleFor(x => x.Instock)
                        .GreaterThanOrEqualTo(0)
                        .WithMessage("- Instock amount must be greater or equal to 0 !");
                });
            });
        }
    }
}