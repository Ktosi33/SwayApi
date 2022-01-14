using FluentValidation;

namespace SwayApi.Models.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator(SwayDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Pole email nie może być puste");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Pole hasło nie może być puste");
        }
    }
}
