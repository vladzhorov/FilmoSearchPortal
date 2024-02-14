using FilmoSearchPortal.API.ViewModels.User;
using FilmoSearchPortal.DAL;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.IntegrationTest.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace FilmoSearchPortal.IntegrationTest.IntegrationTest
{
    public class UserControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly TestingWebAppFactory<Program> _factory;
        private readonly Guid _userId = Guid.NewGuid();

        public UserControllerTests(TestingWebAppFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            AddTestData();
        }

        public void AddTestData()
        {
            // Arrange
            var usersToAdd = new List<UserEntity>
            {
                new UserEntity { Id = _userId, Username = "User 1" },
                new UserEntity { Id = Guid.NewGuid(),  Username = "User 2" }
            };

            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Users.AddRange(usersToAdd);
                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAll_ReturnsListOfUserViewModels()
        {

            // Act
            var response = await _client.GetAsync($"/api/users");
            response.EnsureSuccessStatusCode();

            var userViewModels = await response.Content.ReadFromJsonAsync<IEnumerable<UserViewModel>>();

            // Assert
            Assert.NotNull(userViewModels);
            Assert.NotEmpty(userViewModels);
        }

        [Fact]
        public async Task GetById_ReturnsUserViewModel()
        {
            // Arrange
            var userId = _userId;

            // Act
            var response = await _client.GetAsync($"/api/users/{userId}");
            response.EnsureSuccessStatusCode();

            var userViewModel = await response.Content.ReadFromJsonAsync<UserViewModel>();

            // Assert
            Assert.NotNull(userViewModel);
            Assert.Equal(userId, userViewModel.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedUserViewModel()
        {
            // Arrange
            var createViewModel = new CreateUserViewModel
            {
                Username = "New User",

            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/users", createViewModel);
            response.EnsureSuccessStatusCode();

            var createdUserViewModel = await response.Content.ReadFromJsonAsync<UserViewModel>();

            // Assert
            Assert.NotNull(createdUserViewModel);
            Assert.Equal(createViewModel.Username, createdUserViewModel.Username);

        }
        [Fact]
        public async Task Delete_RemovesUserFromDatabase()
        {
            // Arrange
            var userId = _userId;

            // Act
            var response = await _client.DeleteAsync($"/api/users/{userId}");
            response.EnsureSuccessStatusCode();

            // Assert
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var deletedUser = await dbContext.Users.FindAsync(userId);
                Assert.Null(deletedUser);
            }
        }
    }
}
