using FilmoSearchPortal.API.Validators.User;
using FilmoSearchPortal.API.ViewModels.User;

namespace FilmoSearchPortal.UnitTest.UnitTest
{
    public class CreateUserViewModelValidatorTests
    {
        private readonly CreateUserViewModelValidator _validator;

        public CreateUserViewModelValidatorTests()
        {
            _validator = new CreateUserViewModelValidator();
        }

        [Fact]
        public void Username_WhenNull()
        {
            var model = new CreateUserViewModel { Username = null };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateUserViewModel.Username));
        }


    }
}