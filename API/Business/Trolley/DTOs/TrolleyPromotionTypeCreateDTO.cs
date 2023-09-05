

using FluentValidation;

namespace Business.Trolley.DTOs
{
    public class TrolleyPromotionTypeCreateDTO
    {
        public int TrolleyPromotionTypeId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }



    public class TrolleyPromotionTypeCreateDTO_V : AbstractValidator<TrolleyPromotionTypeCreateDTO>
    {
        public TrolleyPromotionTypeCreateDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Trolley Promotion Type data model must NOT be NULL !");
            When(x => x != null, () => {
                RuleFor(x => x.TrolleyPromotionTypeId)
                    .GreaterThan(0)
                    .WithMessage("- Trolley Promotion Type Id must be greater than 0 !");
                When(x => !string.IsNullOrWhiteSpace(x.Description), () => {
                    RuleFor(x => x.Description)
                    .MaximumLength(100)
                    .WithMessage("- Description should NOT be longer than 100 characters !");
                });
                When(x => !string.IsNullOrWhiteSpace(x.Description), () => {
                    RuleFor(x => x.Name)
                    .MinimumLength(5)
                    .MaximumLength(20)
                    .WithMessage("- Name should NOT be shorter than 5 and longer than 20 characters !");
                });
            });
        }
    }
}
