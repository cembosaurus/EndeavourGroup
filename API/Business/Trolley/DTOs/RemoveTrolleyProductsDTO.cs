using FluentValidation;



namespace Business.Trolley.DTOs
{
    public class RemoveTrolleyProductsDTO
    {
        public int? UserId { get; set; }
        public IEnumerable<TrolleyProductUpdateDTO> Products { get; set; }

    }



    public class RemoveTrolleyProductsDTO_V : AbstractValidator<RemoveTrolleyProductsDTO>
    {
        public RemoveTrolleyProductsDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Remove Trolley Products command model must NOT be NULL !");
            When(x => x != null, () => {
                When(x => x.UserId == null, () =>
                {
                    RuleFor(x => x.Products)
                    .NotNull()
                    .WithMessage("- Products must NOT be NULL !")
                    .NotEmpty()
                    .WithMessage("- Products collection must NOT be empty !");
                    RuleForEach(x => x.Products)
                        .ChildRules(x =>
                        {
                            x.RuleFor(x => x.ProductId)
                                .GreaterThan(0)
                                .WithMessage("- Product Id must be greater than 0 !");
                        });
                    RuleForEach(x => x.Products)
                        .ChildRules(x =>
                        {
                            x.RuleFor(x => x.Amount)
                                .GreaterThan(0)
                                .WithMessage("- Amount must be greater than 0 !");
                        });
                });
                When(x => x.UserId != null, () => {
                    RuleFor(x => x.UserId)
                    .GreaterThan(0)
                    .WithMessage("- User Id must be greater than 0 !");
                });
            });
        }
    }

}
