using FilmoSearchPortal.API.Validators.Review;
using FilmoSearchPortal.API.ViewModels.Review;

namespace FilmoSearchPortal.UnitTest.UnitTest
{
    public class CreateReviewViewModelValidatorTests
    {
        private readonly CreateReviewViewModelValidator _validator;

        public CreateReviewViewModelValidatorTests()
        {
            _validator = new CreateReviewViewModelValidator();
        }

        [Fact]
        public void Title_WhenEmpty()
        {
            var model = new CreateReviewViewModel { Title = "" };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateReviewViewModel.Title));
        }

        [Fact]
        public void Title_WhenNull()
        {
            var model = new CreateReviewViewModel { Title = null };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateReviewViewModel.Title));
        }


        [Fact]
        public void Description_WhenEmpty()
        {
            var model = new CreateReviewViewModel { Description = "" };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateReviewViewModel.Description));
        }

        [Fact]
        public void Description_WhenNull()
        {
            var model = new CreateReviewViewModel { Description = null };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateReviewViewModel.Description));
        }

        [Fact]
        public void Stars_WhenBelowRange()
        {
            var model = new CreateReviewViewModel { Stars = 0 };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateReviewViewModel.Stars));
        }

        [Fact]
        public void Stars_WhenAboveRange()
        {
            var model = new CreateReviewViewModel { Stars = 6 };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateReviewViewModel.Stars));
        }

        [Fact]
        public void FilmId_WhenEmpty()
        {
            var model = new CreateReviewViewModel { FilmId = Guid.Empty };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateReviewViewModel.FilmId));
        }
    }
}
