using FilmoSearchPortal.API.ViewModels.Film;
using FluentValidation;

namespace FilmoSearchPortal.API.Validators.Film
{
    public class UpdateFilmViewModelValidator : AbstractValidator<UpdateFilmViewModel>
    {
        public UpdateFilmViewModelValidator()
        {
            RuleFor(model => model.Title).NotEmpty().MaximumLength(100);
            RuleFor(model => model.Genre).IsInEnum();
            RuleFor(model => model.ReleaseDate).NotEmpty().GreaterThan(DateTime.MinValue);
        }
    }
}
