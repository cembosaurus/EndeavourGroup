using FluentValidation;



namespace Business.Trolley.DTOs
{
    public class ExistsTrolleyByTrolleyIdDTO
    {
        public Guid TrolleyId { get; set; }

    }




    public class ExistsTrolleyByTrolleyIdDTO_V : AbstractValidator<ExistsTrolleyByTrolleyIdDTO>
    {
        public ExistsTrolleyByTrolleyIdDTO_V()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("- Exists Trolley By Trolley Id query model must NOT be NULL !");
            When(x => x != null, () => {
                RuleFor(x => x.TrolleyId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("- Trolley Id must not be empty !");
            });
        }
    }

}
