using AutoMapper;
using FilmoSearchPortal.API.Controllers;
using FilmoSearchPortal.API.ViewModels.User;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.UnitTest.TestData;
using Moq;

namespace FilmoSearchPortal.UnitTest.UnitTest
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMapper> _mapper;

        public UserControllerTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAll_ReturnsListOfUserViewModels()
        {
            // Arrange
            var (users, userViewModels) = UserControllerTestHelper.TestGetAll();
            var cancellationToken = new CancellationToken();
            _userServiceMock.Setup(service => service.GetAllAsync(cancellationToken)).ReturnsAsync(users);
            _mapper.Setup(mapper => mapper.Map<IEnumerable<UserViewModel>>(users)).Returns(userViewModels);
            var controller = new UserController(_mapper.Object, _userServiceMock.Object);

            // Act
            var result = await controller.GetAll(cancellationToken);

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<UserViewModel>>(result);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetById_ReturnsUserViewModel()
        {
            // Arrange
            var (userId, user, userViewModel) = UserControllerTestHelper.TestGetById();
            var cancellationToken = new CancellationToken();
            _userServiceMock.Setup(service => service.GetByIdAsync(userId, cancellationToken)).ReturnsAsync(user);
            _mapper.Setup(mapper => mapper.Map<UserViewModel>(user)).Returns(userViewModel);
            var controller = new UserController(_mapper.Object, _userServiceMock.Object);

            // Act
            var result = await controller.GetById(userId, cancellationToken);

            // Assert
            var model = Assert.IsType<UserViewModel>(result);
            Assert.Equal(userId, model.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedUserViewModel()
        {
            // Arrange
            var (viewModel, user) = UserControllerTestHelper.TestCreate();
            var cancellationToken = new CancellationToken();
            _userServiceMock.Setup(service => service.CreateAsync(It.IsAny<User>(), cancellationToken)).ReturnsAsync(user);
            _mapper.Setup(mapper => mapper.Map<User>(viewModel)).Returns(new User { Id = user.Id, Username = viewModel.Username });
            _mapper.Setup(mapper => mapper.Map<UserViewModel>(user)).Returns(new UserViewModel { Id = user.Id, Username = user.Username });
            var controller = new UserController(_mapper.Object, _userServiceMock.Object);

            // Act
            var result = await controller.Create(viewModel, cancellationToken);

            // Assert
            var model = Assert.IsType<UserViewModel>(result);
            Assert.Equal(user.Id, model.Id);
            Assert.Equal(viewModel.Username, model.Username);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedUserViewModel()
        {
            // Arrange
            var (userId, viewModel, updatedUser) = UserControllerTestHelper.TestUpdate();
            var cancellationToken = new CancellationToken();
            _userServiceMock.Setup(service => service.UpdateAsync(It.IsAny<User>(), cancellationToken)).ReturnsAsync(updatedUser);
            _mapper.Setup(mapper => mapper.Map<User>(viewModel)).Returns(new User { Id = userId, Username = viewModel.Username });
            _mapper.Setup(mapper => mapper.Map<UserViewModel>(updatedUser)).Returns(new UserViewModel { Id = userId, Username = updatedUser.Username });
            var controller = new UserController(_mapper.Object, _userServiceMock.Object);

            // Act
            var result = await controller.Update(userId, viewModel, cancellationToken);

            // Assert
            var model = Assert.IsType<UserViewModel>(result);
            Assert.Equal(userId, model.Id);
            Assert.Equal(viewModel.Username, model.Username);
        }
    }
}
