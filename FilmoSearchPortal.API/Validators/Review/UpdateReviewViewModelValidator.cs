using FilmoSearchPortal.API.ViewModels.Review;
using FluentValidation;

namespace FilmoSearchPortal.API.Validators.Review
{
    public class UpdateReviewViewModelValidator : AbstractValidator<UpdateReviewViewModel>
    {
        public UpdateReviewViewModelValidator()
        {
            RuleFor(model => model.Title).NotEmpty().MaximumLength(100);
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(model => model.Stars).InclusiveBetween(1, 5);

        }
    }
}
