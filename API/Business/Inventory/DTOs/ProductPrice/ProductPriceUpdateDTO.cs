using FluentValidation;

namespace Business.Inventory.DTOs.ProductPrice
{
    public class ProductPriceUpdateDTO
    {
        public decimal? SalePrice { get; set; }
        public decimal? RRP { get; set; }
        public int? DiscountPercent { get; set; }
    }


    public class Validation : AbstractValidator<ProductPriceUpdateDTO>
    {
        public Validation()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Product Price update model must NOT be NULL !");
            When(x => x != null, () => {
                RuleFor(x => x)
                    .Must(x => x.SalePrice.HasValue || x.RRP.HasValue || x.DiscountPercent.HasValue)
                    .WithMessage("- At least one property is required for Product Price update !");

                When(x => x.SalePrice.HasValue, () => {
                    RuleFor(x => x.SalePrice)
                        .GreaterThan(0)
                        .WithMessage("- Sales Price should be HIGHER than '0' !");
                });

                When(x => x.RRP.HasValue, () => {
                    RuleFor(x => x.RRP)
                        .GreaterThan(0)
                        .WithMessage("- RRP should be HIGHER than '0' !");
                });

                When(x => x.DiscountPercent.HasValue, () => {
                    RuleFor(x => x.DiscountPercent)
                        .InclusiveBetween(0, 100)
                        .WithMessage("- Discount percent should be between '0' and '100' !");
                });
            });
        }
    }
}
