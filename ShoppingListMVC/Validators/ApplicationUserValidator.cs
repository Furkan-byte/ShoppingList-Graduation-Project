using FluentValidation;
using Models;

namespace ShoppingListMVC.Validators
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
    {
        public ApplicationUserValidator()
        {
            RuleFor(a => a.Email).NotEmpty().WithMessage("Email can not be empty")
                .EmailAddress().WithMessage("Try a valid e-mail account.");
            RuleFor(a => a.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(a => a.Surname).NotEmpty().WithMessage("Surname can not be empty");
        }
    }
}
