using FilmoSearchPortal.API.ViewModels.Actor;
using FluentValidation;

namespace FilmoSearchPortal.API.Validators.Actor
{
    public class CreateActorViewModelValidator : AbstractValidator<CreateActorViewModel>
    {
        public CreateActorViewModelValidator()
        {
            RuleFor(model => model.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(model => model.LastName).NotEmpty().MaximumLength(50);
            RuleFor(model => model.DateOfBirth).NotEmpty().GreaterThan(DateTime.MinValue);
        }
    }
}
