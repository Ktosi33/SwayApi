﻿using FluentValidation;
namespace SwayApi.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(SwayDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            
            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password).WithMessage("Podane hasła nie są sobie równe") ;

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if(emailInUse)
                    {
                        context.AddFailure("Email", "Podany mail jest już zajęty");
                    }
                });
            
        }



    }
}
