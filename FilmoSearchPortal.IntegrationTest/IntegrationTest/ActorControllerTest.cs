using FilmoSearchPortal.API.ViewModels.Actor;
using FilmoSearchPortal.DAL;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.IntegrationTest.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.IntegrationTest.IntegrationTest
{
    public class ActorControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly TestingWebAppFactory<Program> _factory;
        private readonly Guid _actorId1 = Guid.NewGuid();

        public ActorControllerTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
            AddTestData();
        }

        public void AddTestData()
        {
            var actorsToAdd = new List<ActorEntity>
            {
                new ActorEntity { Id = _actorId1, FirstName = "Actor 1", LastName = "Actor 1" },
                new ActorEntity { Id = Guid.NewGuid(), FirstName = "Actor 2", LastName = "Actor 2" }
            };

            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Actors.AddRange(actorsToAdd);
                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAll_ReturnsListOfActorViewModels()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;

            // Act
            var response = await _client.GetAsync($"/api/actors?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            var actorViewModels = await response.Content.ReadFromJsonAsync<IEnumerable<ActorViewModel>>();

            // Assert
            Assert.NotNull(actorViewModels);
            Assert.NotEmpty(actorViewModels);
        }

        [Fact]
        public async Task GetById_ReturnsActorViewModel()
        {
            // Arrange
            var actorId = _actorId1;

            // Act
            var response = await _client.GetAsync($"/api/actors/{actorId}");
            response.EnsureSuccessStatusCode();

            var actorViewModel = await response.Content.ReadFromJsonAsync<ActorViewModel>();

            // Assert
            Assert.NotNull(actorViewModel);
            Assert.Equal(actorId, actorViewModel.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedActorViewModel()
        {
            // Arrange
            var createViewModel = new CreateActorViewModel
            {
                FirstName = "New Actor",
                LastName = "New Actor",
                DateOfBirth = DateTime.Now,
                ActorStatus = ActorStatus.Active
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/actors", createViewModel);
            response.EnsureSuccessStatusCode();

            var createdActorViewModel = await response.Content.ReadFromJsonAsync<ActorViewModel>();

            // Assert
            Assert.NotNull(createdActorViewModel);
            Assert.Equal(createViewModel.FirstName, createdActorViewModel.FirstName);
            Assert.Equal(createViewModel.LastName, createdActorViewModel.LastName);
            Assert.Equal(createViewModel.DateOfBirth, createdActorViewModel.DateOfBirth);
            Assert.Equal(createViewModel.ActorStatus, createdActorViewModel.ActorStatus);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedActorViewModel()
        {
            // Arrange
            var actorId = _actorId1;
            var updateViewModel = new UpdateActorViewModel
            {
                FirstName = "Updated First Name",
                LastName = "Updated Last Name",
                DateOfBirth = DateTime.Now.AddYears(2022),
                ActorStatus = ActorStatus.Retired
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/actors/{actorId}", updateViewModel);
            response.EnsureSuccessStatusCode();

            var updatedActorViewModel = await response.Content.ReadFromJsonAsync<ActorViewModel>();

            // Assert
            Assert.NotNull(updatedActorViewModel);
            Assert.Equal(updateViewModel.FirstName, updatedActorViewModel.FirstName);
            Assert.Equal(updateViewModel.LastName, updatedActorViewModel.LastName);
            Assert.Equal(updateViewModel.DateOfBirth, updatedActorViewModel.DateOfBirth);
            Assert.Equal(updateViewModel.ActorStatus, updatedActorViewModel.ActorStatus);


        }

        [Fact]
        public async Task Delete_RemovesActorFromDatabase()
        {
            // Arrange
            var actorId = _actorId1;

            // Act
            var response = await _client.DeleteAsync($"/api/actors/{actorId}");
            response.EnsureSuccessStatusCode();

            // Assert
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var deletedActor = await dbContext.Actors.FindAsync(actorId);
                Assert.Null(deletedActor);
            }
        }
    }
}
