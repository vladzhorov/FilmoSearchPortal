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
    public class ReviewServiceTests
    {
        private readonly Mock<IReviewRepository> _reviewRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ReviewService _reviewService;

        public ReviewServiceTests()
        {
            _reviewRepositoryMock = new Mock<IReviewRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelMappingProfile>();
            });
            _mapper = config.CreateMapper();
            _reviewService = new ReviewService(_reviewRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllReviews_ReturnsListOfReviewModels()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string title = "Review Title";
            int stars = 5;

            var (reviewsEntities, reviewModels) = ReviewTestData.TestGetAllReviews();
            _reviewRepositoryMock.Setup(x => x.GetAllAsync(pageNumber, pageSize, title, stars, It.IsAny<CancellationToken>())).ReturnsAsync(reviewsEntities);

            // Act
            var result = await _reviewService.GetAllAsync(pageNumber, pageSize, title, stars, CancellationToken.None);

            // Assert
            Assert.Equal(reviewModels.Count, result.Count());
            Assert.Equal(reviewModels[0].Id, result.ElementAt(0).Id);
            Assert.Equal(reviewModels[1].Title, result.ElementAt(1).Title);
            // Additional assertions for other properties
        }

        [Fact]
        public async Task GetReviewById_ExistingReviewId_ReturnsReviewModel()
        {
            // Arrange
            var (reviewId, reviewEntity, reviewModel) = ReviewTestData.CreateReview();
            _reviewRepositoryMock.Setup(x => x.GetByIdAsync(reviewId, CancellationToken.None)).ReturnsAsync(reviewEntity);

            // Act
            var result = await _reviewService.GetByIdAsync(reviewId, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reviewModel.Id, result.Id);
            Assert.Equal(reviewModel.Title, result.Title);
            // Additional assertions for other properties
        }

        [Fact]
        public async Task CreateReview_ValidReviewModel_CallsAddAsyncWithCorrectReviewEntity()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var newReviewModel = new Review { Title = "New Review", Stars = 4 };
            var newReviewEntity = new ReviewEntity { Id = Guid.NewGuid(), Title = "New Review", Stars = 4 };
            _reviewRepositoryMock.Setup(x => x.AddAsync(It.IsNotNull<ReviewEntity>(), cancellationToken)).ReturnsAsync(newReviewEntity);

            // Act
            await _reviewService.CreateAsync(newReviewModel, cancellationToken);

            // Assert
            _reviewRepositoryMock.Verify(x => x.AddAsync(It.Is<ReviewEntity>(reviewEntity =>
                reviewEntity.Title == newReviewModel.Title &&
                reviewEntity.Stars == newReviewModel.Stars), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task UpdateReview_ExistingReviewId_ValidReviewModel_CallsUpdateAsyncWithCorrectReviewEntity()
        {
            // Arrange
            var reviewId = Guid.NewGuid();
            var existingReviewEntity = new ReviewEntity { Id = reviewId, Title = "Existing Review", Stars = 3 };
            var updatedReviewModel = new Review { Id = existingReviewEntity.Id, Title = "Updated Review", Stars = 5 };

            _reviewRepositoryMock.Setup(x => x.GetByIdAsync(reviewId, CancellationToken.None)).ReturnsAsync(existingReviewEntity);
            _reviewRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<ReviewEntity>(), CancellationToken.None)).ReturnsAsync(existingReviewEntity);

            // Act
            await _reviewService.UpdateAsync(updatedReviewModel, CancellationToken.None);

            // Assert
            _reviewRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<ReviewEntity>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task UpdateReview_NonExistingReviewId_ReturnsNull()
        {
            var reviewId = Guid.NewGuid();
            _reviewRepositoryMock.Setup(x => x.GetByIdAsync(reviewId, CancellationToken.None)).ReturnsAsync((ReviewEntity)null);

            // Act
            var result = await _reviewService.UpdateAsync(new Review { Id = reviewId }, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteReview_ExistingReviewId_CallsDeleteAsyncWithCorrectReviewEntity()
        {
            // Arrange
            var reviewId = Guid.NewGuid();
            var existingReview = new ReviewEntity { Id = reviewId, Title = "Review Title", Stars = 4 };
            _reviewRepositoryMock.Setup(x => x.GetByIdAsync(reviewId, CancellationToken.None)).ReturnsAsync(existingReview);
            _reviewRepositoryMock.Setup(x => x.DeleteAsync(existingReview, CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            await _reviewService.DeleteAsync(reviewId, CancellationToken.None);

            // Assert
            _reviewRepositoryMock.Verify(x => x.DeleteAsync(existingReview, CancellationToken.None), Times.Once);
        }

    }
}
