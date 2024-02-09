using FilmoSearchPortal.API.ViewModels.Film;
using FluentValidation;

namespace FilmoSearchPortal.API.Validators.Film
{
    public class CreateFilmViewModelValidator : AbstractValidator<CreateFilmViewModel>
    {
        public CreateFilmViewModelValidator()
        {
            RuleFor(model => model.Title).NotEmpty().MaximumLength(100);
            RuleFor(model => model.Genre).NotEmpty().MaximumLength(50);
            RuleFor(model => model.ReleaseDate).NotEmpty().GreaterThan(DateTime.MinValue);
        }
    }
}
