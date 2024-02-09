using FilmoSearchPortal.API.ViewModels.User;
using FluentValidation;

namespace FilmoSearchPortal.API.Validators.User
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(user => user.Username).NotEmpty().MaximumLength(50);

        }
    }
}
