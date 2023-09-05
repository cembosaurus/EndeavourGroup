using FluentValidation;



namespace Business.Trolley.DTOs
{
    public class DeleteTrolleyDTO
    {
        public int UserId { get; set; }

    }




    public class DeleteTrolleyDTO_V : AbstractValidator<DeleteTrolleyDTO>
    {
        public DeleteTrolleyDTO_V()
        {
            RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("- User Id must be greater than 0 !");
        }

    }

}
