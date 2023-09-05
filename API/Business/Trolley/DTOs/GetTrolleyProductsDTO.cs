using FluentValidation;



namespace Business.Trolley.DTOs
{
    public class GetTrolleyProductsDTO
    {
        public int UserId { get; set; }

    }





    public class GetTrolleyProductsDTO_V : AbstractValidator<GetTrolleyProductsDTO>
    {
        public GetTrolleyProductsDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Get Trolley Products query model must NOT be NULL !");
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("- User Id must be greater than 0 !");
        }
    }

}
