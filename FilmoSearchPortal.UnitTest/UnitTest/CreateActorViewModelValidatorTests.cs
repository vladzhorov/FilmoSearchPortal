using FilmoSearchPortal.API.Validators.Actor;
using FilmoSearchPortal.API.ViewModels.Actor;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.UnitTest.UnitTest
{
    public class CreateActorViewModelValidatorTests
    {
        private readonly CreateActorViewModelValidator _validator;

        public CreateActorViewModelValidatorTests()
        {
            _validator = new CreateActorViewModelValidator();
        }

        [Fact]
        public void FirstName_WhenEmpty()
        {
            var model = new CreateActorViewModel { FirstName = "" };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateActorViewModel.FirstName));
        }

        [Fact]
        public void FirstName_WhenNull()
        {
            var model = new CreateActorViewModel { FirstName = null };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateActorViewModel.FirstName));
        }


        [Fact]
        public void LastName_WhenEmpty()
        {
            var model = new CreateActorViewModel { LastName = "" };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateActorViewModel.LastName));
        }

        [Fact]
        public void LastName_WhenNull()
        {
            var model = new CreateActorViewModel { LastName = null };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateActorViewModel.LastName));
        }

        [Fact]
        public void DateOfBirth_WhenNotSpecified()
        {
            var model = new CreateActorViewModel { DateOfBirth = DateTime.MinValue };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateActorViewModel.DateOfBirth));
        }

        [Fact]
        public void ActorStatus_WhenNotSpecified()
        {
            var model = new CreateActorViewModel { ActorStatus = (ActorStatus)int.MaxValue };
            var result = _validator.Validate(model);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateActorViewModel.ActorStatus));
        }
    }
}
