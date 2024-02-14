using AutoMapper;
using FilmoSearchPortal.API.Mapping;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.BLL.Services;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;
using FilmoSearchPortal.UnitTest.TestData;
using Moq;

namespace FilmoSearchPortal.BLL.UnitTest.UnitTest
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelMappingProfile>();
            });
            _mapper = config.CreateMapper();
            _userService = new UserService(_userRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsListOfUserModels()
        {
            // Arrange
            var (usersEntities, userModels) = UserTestData.TestGetAllUsers();
            _userRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(usersEntities);

            // Act
            var result = await _userService.GetAllAsync(CancellationToken.None);

            // Assert
            Assert.Equal(userModels.Count, result.Count());
            Assert.Equal(userModels[0].Id, result.ElementAt(0).Id);
            Assert.Equal(userModels[1].Username, result.ElementAt(1).Username);
            // Additional assertions for other properties
        }

        [Fact]
        public async Task GetUserById_ExistingUserId_ReturnsUserModel()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userEntity = new UserEntity
            {
                Id = userId,
                Username = "TestUser",
            };
            var userModel = new User
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
            };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId, CancellationToken.None)).ReturnsAsync(userEntity);

            // Act
            var result = await _userService.GetByIdAsync(userId, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userModel.Id, result.Id);
            Assert.Equal(userModel.Username, result.Username);
        }




        [Fact]
        public async Task GetUserById_NonExistingUserId_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId, CancellationToken.None)).ReturnsAsync((UserEntity)null);

            // Act
            var result = await _userService.GetByIdAsync(userId, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateUser_ValidUserModel_CallsAddAsyncWithCorrectUserEntity()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var (userModel, userEntity) = UserTestData.CreateUser();
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsNotNull<UserEntity>(), cancellationToken)).ReturnsAsync(userEntity);

            // Act
            var result = await _userService.CreateAsync(userModel, cancellationToken);

            // Assert
            _userRepositoryMock.Verify(x => x.AddAsync(It.Is<UserEntity>(userEntity =>
                userEntity.Username == userModel.Username), cancellationToken), Times.Once);
            Assert.Equal(userModel.Id, result.Id);
            Assert.Equal(userModel.Username, result.Username);
            // Additional assertions for other properties
        }
        [Fact]
        public async Task UpdateUser_NonExistingUserId_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<UserEntity>(), CancellationToken.None)).ReturnsAsync((UserEntity)null);

            // Act
            var result = await _userService.UpdateAsync(new User { Id = userId }, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _userRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<UserEntity>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_ExistingUserId_ValidUserModel_CallsUpdateAsyncWithCorrectUserEntity()
        {
            // Arrange
            var (userId, existingUserEntity, updatedUserModel) = UserTestData.CreateExistingUser();
            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId, CancellationToken.None)).ReturnsAsync(existingUserEntity);
            _userRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<UserEntity>(), CancellationToken.None)).ReturnsAsync(existingUserEntity);

            // Act
            await _userService.UpdateAsync(updatedUserModel, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<UserEntity>(), CancellationToken.None), Times.Once);
            Assert.NotEqual(updatedUserModel.Username, existingUserEntity.Username);
            // Additional assertions for other properties
        }



        [Fact]
        public async Task DeleteUser_ExistingUserId_CallsDeleteAsyncWithCorrectUserEntity()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var existingUserEntity = new UserEntity { Id = userId, Username = "TestUser" };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId, CancellationToken.None)).ReturnsAsync(existingUserEntity);
            _userRepositoryMock.Setup(x => x.DeleteAsync(existingUserEntity, CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            await _userService.DeleteAsync(userId, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(x => x.DeleteAsync(existingUserEntity, CancellationToken.None), Times.Once);
        }

    }
}