

using FluentValidation;

namespace Business.Trolley.DTOs
{
    public record TrolleyPromotionCreateDTO
    {
        public bool IsOn { get; set; }
        public int TrolleyPromotionTypeId { get; set; }
        public int ProductId { get; set; }
        public int PriceLevel { get; set; }
        public int DiscountPercent { get; set; }
        public TrolleyPromotionTypeCreateDTO TrolleyPromotionTypeCreateDTO { get; set; }
    }



    public class TrolleyPromotionCreateDTO_V : AbstractValidator<TrolleyPromotionCreateDTO>
    {
        public TrolleyPromotionCreateDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Trolley Promotion data model must NOT be NULL !");
            When(x => x != null, () => {
                RuleFor(x => x.TrolleyPromotionTypeId)
                    .GreaterThan(0)
                    .WithMessage("- Trolley Promotion Type Id must be greater than 0 !");
                RuleFor(x => x.ProductId)
                    .GreaterThan(0)
                    .WithMessage("- Trolley Product Id must be greater than 0 !");
                RuleFor(x => x.PriceLevel)
                    .GreaterThan(0)
                    .WithMessage("- Trolley Price Level must be greater than 0 !");
                RuleFor(x => x.DiscountPercent)
                    .GreaterThan(0)
                    .WithMessage("- Trolley Discount Percent must be greater than 0 !");
                RuleFor(x => x.TrolleyPromotionTypeCreateDTO)
                    .NotNull()
                    .WithMessage("- Trolley Promotion Type data model must NOT be NULL !");
            });
        }
    }
}
