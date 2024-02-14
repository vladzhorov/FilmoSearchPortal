using FilmoSearchPortal.API.Validators.Film;
using FilmoSearchPortal.API.ViewModels.Film;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.UnitTest.UnitTest
{
    public class CreateFilmViewModelValidatorTests
    {
        private readonly CreateFilmViewModelValidator _validator;

        public CreateFilmViewModelValidatorTests()
        {
            _validator = new CreateFilmViewModelValidator();
        }

        [Fact]
        public void Title_WhenEmpty()
        {
            var model = new CreateFilmViewModel { Title = "" };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateFilmViewModel.Title));
        }

        [Fact]
        public void Title_WhenNull()
        {
            var model = new CreateFilmViewModel { Title = null };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateFilmViewModel.Title));
        }

        [Fact]
        public void Genre_WhenNotSpecified()
        {
            var model = new CreateFilmViewModel { Genre = (Genre)int.MaxValue };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateFilmViewModel.Genre));
        }

        [Fact]
        public void ReleaseDate_WhenNotSpecified()
        {
            var model = new CreateFilmViewModel { ReleaseDate = DateTime.MinValue };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateFilmViewModel.ReleaseDate));
        }
    }
}
