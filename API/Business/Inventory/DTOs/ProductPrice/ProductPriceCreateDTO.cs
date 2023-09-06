using FluentValidation;

namespace Business.Inventory.DTOs.ProductPrice
{
    public class ProductPriceCreateDTO
    {
        public decimal SalePrice { get; set; }
        public decimal? RRP { get; set; }
        public int? DiscountPercent { get; set; }
    }

    public class ProductPriceCreateDTO_V : AbstractValidator<ProductPriceCreateDTO>
    {
        public ProductPriceCreateDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Product Price create data model must NOT be NULL !");
            When(x => x != null, () => {
                RuleFor(x => x.SalePrice)
                    .GreaterThan(0)
                    .WithMessage("- SalePrice must be greater than 0 !");

                When(x => x.RRP.HasValue, () => {
                    RuleFor(x => x.RRP)
                    .GreaterThan(0)
                    .WithMessage("- RRP must be greater than 0 !");
                });
                When(x => x.DiscountPercent.HasValue, () => {
                    RuleFor(x => x.DiscountPercent)
                    .InclusiveBetween(1, 100)
                    .WithMessage("- Discount Percent must be in range 1 - 100 !");
                });
            });
        }
    }
}
