using FluentValidation;



namespace Business.Trolley.DTOs
{
    public class CreateTrolleyDTO
    {
        public int UserId { get; set; }
    
    }



    public class CreateTrolleyDTO_V : AbstractValidator<GetTrolleyProductsDTO>
    {
        public CreateTrolleyDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Get Trolley Products query model must NOT be NULL !");
            When(x => x != null, () => {
                When(x => x.UserId != null, () => {
                    RuleFor(x => x.UserId)
                    .GreaterThan(0)
                    .WithMessage("- User Id must be greater than 0 !");
                });
            });
        }
    }
}
