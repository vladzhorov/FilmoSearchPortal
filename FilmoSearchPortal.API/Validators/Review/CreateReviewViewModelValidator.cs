using FilmoSearchPortal.API.ViewModels.Review;
using FluentValidation;

namespace FilmoSearchPortal.API.Validators.Review
{
    public class CreateReviewViewModelValidator : AbstractValidator<CreateReviewViewModel>
    {
        public CreateReviewViewModelValidator()
        {
            RuleFor(model => model.Title).NotEmpty().MaximumLength(100);
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(model => model.Stars).InclusiveBetween(1, 5);
            RuleFor(model => model.FilmId).NotEmpty();
        }
    }
}
