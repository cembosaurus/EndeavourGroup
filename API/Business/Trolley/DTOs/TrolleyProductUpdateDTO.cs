using FluentValidation;



namespace Business.Trolley.DTOs
{
    public record TrolleyProductUpdateDTO
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }


    public class TrolleyProductUpdateDTO_V : AbstractValidator<TrolleyProductUpdateDTO>
    {
        public TrolleyProductUpdateDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Trolley Product update data model must NOT be NULL !");
            When(x => x != null, () => {
                RuleFor(x => x.ProductId)
                    .GreaterThan(0)
                    .WithMessage("- Product Id must be greater than 0 !");
                RuleFor(x => x.Amount)
                    .GreaterThan(0)
                    .WithMessage("- Amount must be greater than 0 !");
            });
        }
    }
}
