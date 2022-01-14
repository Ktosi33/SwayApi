using FluentValidation;

namespace SwayApi.Models.Validators
{
    public class ToDoTaskDtoValidator : AbstractValidator<ToDoTaskDto>
    {
        private readonly int maxTitleChars = 60;
        private readonly int maxDescChars = 500;
        
        public ToDoTaskDtoValidator(SwayDbContext dbContext)
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Pole Tytuł nie może być puste")
               .MaximumLength(maxTitleChars).WithMessage($"Pole 'Tytuł' może mieć najwięcej {maxTitleChars} znaków");

            
            RuleFor(x => x.Description).MaximumLength(maxDescChars).WithMessage($"Pole 'Opis' może mieć najwięcej {maxDescChars} znaków");


        }
    }
}
