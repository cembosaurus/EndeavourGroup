using FluentValidation;



namespace Business.Trolley.DTOs
{
    public class DeleteTrolleyProductsDTO
    {
        public int? UserId { get; set; }
        public IEnumerable<int> Products { get; set; }

    }



    public class DeleteTrolleyProductsDTO_V : AbstractValidator<DeleteTrolleyProductsDTO>
    {
        public DeleteTrolleyProductsDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Delete Trolley Products command model must NOT be NULL !");
            When(x => x != null, () => {
                When(x => x.UserId == null, () =>
                {
                    RuleFor(x => x.Products)
                    .NotNull()
                    .WithMessage("- Products must NOT be NULL !")
                    .NotEmpty()
                    .WithMessage("- Products collection must NOT be empty !");
                    RuleForEach(x => x.Products)
                        .GreaterThan(0)
                        .WithMessage("- Product Id must be greater than 0 !");
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
