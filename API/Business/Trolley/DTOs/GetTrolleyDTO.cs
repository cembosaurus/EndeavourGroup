using FluentValidation;



namespace Business.Trolley.DTOs
{
    public record GetTrolleyDTO
    {
        public int? UserId { get; set; }

    }




    public class GetTrolleyDTO_V : AbstractValidator<GetTrolleyDTO>
    {
        public GetTrolleyDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Get Trolley query model must NOT be NULL !");
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
