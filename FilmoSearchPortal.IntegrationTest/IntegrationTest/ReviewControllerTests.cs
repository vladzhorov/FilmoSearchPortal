using FilmoSearchPortal.API.ViewModels.Review;
using FilmoSearchPortal.DAL;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.IntegrationTest.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace FilmoSearchPortal.API.Tests.Controllers
{
    public class ReviewControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly TestingWebAppFactory<Program> _factory;
        private readonly Guid _reviewId = Guid.NewGuid();
        private readonly Guid _filmId;
        private readonly Guid _userId;

        public ReviewControllerTests(TestingWebAppFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            (_filmId, _userId) = AddTestData();
        }

        public (Guid, Guid) AddTestData()
        {
            // Arrange
            var filmId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var reviewsToAdd = new List<ReviewEntity>
            {
                new ReviewEntity { Id = _reviewId, Title = "Review 1", Description = "Description 1", FilmId = filmId, UserId = userId },
                new ReviewEntity { Id = Guid.NewGuid(), Title = "Review 2", Description = "Description 2", FilmId = Guid.NewGuid(), UserId = Guid.NewGuid() }
            };

            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Reviews.AddRange(reviewsToAdd);
                dbContext.SaveChanges();
            }

            return (filmId, userId);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfReviewViewModels()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;

            // Act
            var response = await _client.GetAsync($"/api/reviews?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            var reviewViewModels = await response.Content.ReadFromJsonAsync<IEnumerable<ReviewViewModel>>();

            // Assert
            Assert.NotNull(reviewViewModels);
            Assert.NotEmpty(reviewViewModels);
        }

        [Fact]
        public async Task GetById_ReturnsReviewViewModel()
        {
            // Arrange
            var reviewId = _reviewId;

            // Act
            var response = await _client.GetAsync($"/api/reviews/{reviewId}");
            response.EnsureSuccessStatusCode();

            var reviewViewModel = await response.Content.ReadFromJsonAsync<ReviewViewModel>();

            // Assert
            Assert.NotNull(reviewViewModel);
            Assert.Equal(reviewId, reviewViewModel.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedReviewViewModel()
        {
            // Arrange
            var createViewModel = new CreateReviewViewModel
            {
                Title = "New Review",
                Description = "Description for new review",
                Stars = 3,
                FilmId = _filmId,
                UserId = _userId
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/reviews", createViewModel);
            response.EnsureSuccessStatusCode();

            var createdReviewViewModel = await response.Content.ReadFromJsonAsync<ReviewViewModel>();

            // Assert
            Assert.NotNull(createdReviewViewModel);
            Assert.Equal(createViewModel.Title, createdReviewViewModel.Title);
            Assert.Equal(createViewModel.Description, createdReviewViewModel.Description);
            Assert.Equal(createViewModel.Stars, createdReviewViewModel.Stars);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedReviewViewModel()
        {
            // Arrange
            var reviewId = _reviewId;
            var updateViewModel = new UpdateReviewViewModel
            {
                Title = "Updated Review Title",
                Description = "Updated Description",
                Stars = 3
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/reviews/{reviewId}", updateViewModel);
            response.EnsureSuccessStatusCode();

            var updatedReviewViewModel = await response.Content.ReadFromJsonAsync<ReviewViewModel>();

            // Assert
            Assert.NotNull(updatedReviewViewModel);
            Assert.Equal(updateViewModel.Title, updatedReviewViewModel.Title);
            Assert.Equal(updateViewModel.Description, updatedReviewViewModel.Description);
            Assert.Equal(updateViewModel.Stars, updatedReviewViewModel.Stars);
        }

        [Fact]
        public async Task Delete_RemovesReviewFromDatabase()
        {
            // Arrange
            var reviewId = _reviewId;

            // Act
            var response = await _client.DeleteAsync($"/api/reviews/{reviewId}");
            response.EnsureSuccessStatusCode();

            // Assert
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var deletedReview = await dbContext.Reviews.FindAsync(reviewId);
                Assert.Null(deletedReview);
            }
        }
    }
}
