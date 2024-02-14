using FilmoSearchPortal.API.ViewModels.User;
using FluentValidation;

namespace FilmoSearchPortal.API.Validators.User
{
    public class UpdateUserViewModelValidator : AbstractValidator<UpdateUserViewModel>
    {
        public UpdateUserViewModelValidator()
        {
            RuleFor(user => user.Username).NotEmpty().MaximumLength(50);

        }
    }
}
