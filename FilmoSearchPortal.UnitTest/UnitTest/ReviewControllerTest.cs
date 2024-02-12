using AutoMapper;
using FilmoSearchPortal.API.Controllers;
using FilmoSearchPortal.API.ViewModels.Review;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.UnitTest.TestData;
using Moq;

namespace FilmoSearchPortal.UnitTest.UnitTest
{
    public class ReviewControllerTests
    {
        private readonly Mock<IReviewService> _reviewServiceMock;
        private readonly Mock<IMapper> _mapper;

        public ReviewControllerTests()
        {
            _reviewServiceMock = new Mock<IReviewService>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAll_ReturnsListOfReviewViewModels()
        {
            // Arrange
            var (reviews, reviewViewModels) = ReviewControllerTestHelper.TestGetAll();
            var cancellationToken = new CancellationToken();
            _reviewServiceMock.Setup(service => service.GetAllAsync(cancellationToken)).ReturnsAsync(reviews);
            _mapper.Setup(mapper => mapper.Map<IEnumerable<ReviewViewModel>>(reviews)).Returns(reviewViewModels);
            var controller = new ReviewController(_mapper.Object, _reviewServiceMock.Object);

            // Act
            var result = await controller.GetAll(cancellationToken);

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<ReviewViewModel>>(result);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetById_ReturnsReviewViewModel()
        {
            // Arrange
            var (reviewId, review, reviewViewModel) = ReviewControllerTestHelper.TestGetById();
            var cancellationToken = new CancellationToken();
            _reviewServiceMock.Setup(service => service.GetByIdAsync(reviewId, cancellationToken)).ReturnsAsync(review);
            _mapper.Setup(mapper => mapper.Map<ReviewViewModel>(review)).Returns(new ReviewViewModel { Id = reviewId, Title = review.Title });
            var controller = new ReviewController(_mapper.Object, _reviewServiceMock.Object);

            // Act
            var result = await controller.GetById(reviewId, cancellationToken);

            // Assert
            var model = Assert.IsType<ReviewViewModel>(result);
            Assert.Equal(reviewId, model.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedReviewViewModel()
        {
            // Arrange
            var (viewModel, review) = ReviewControllerTestHelper.TestCreate();
            var cancellationToken = new CancellationToken();
            _reviewServiceMock.Setup(service => service.CreateAsync(It.IsAny<Review>(), cancellationToken)).ReturnsAsync(review);
            _mapper.Setup(mapper => mapper.Map<Review>(viewModel)).Returns(new Review { Id = review.Id, Title = viewModel.Title });
            _mapper.Setup(mapper => mapper.Map<ReviewViewModel>(review)).Returns(new ReviewViewModel { Id = review.Id, Title = review.Title });
            var controller = new ReviewController(_mapper.Object, _reviewServiceMock.Object);

            // Act
            var result = await controller.Create(viewModel, cancellationToken);

            // Assert
            var model = Assert.IsType<ReviewViewModel>(result);
            Assert.Equal(review.Id, model.Id);
            Assert.Equal(viewModel.Title, model.Title);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedReviewViewModel()
        {
            // Arrange
            var (reviewId, viewModel, updatedReview) = ReviewControllerTestHelper.TestUpdate();
            var cancellationToken = new CancellationToken();
            _reviewServiceMock.Setup(service => service.UpdateAsync(It.IsAny<Review>(), cancellationToken)).ReturnsAsync(updatedReview);
            _mapper.Setup(mapper => mapper.Map<Review>(viewModel)).Returns(new Review { Id = reviewId, Title = viewModel.Title });
            _mapper.Setup(mapper => mapper.Map<ReviewViewModel>(updatedReview)).Returns(new ReviewViewModel { Id = reviewId, Title = updatedReview.Title });
            var controller = new ReviewController(_mapper.Object, _reviewServiceMock.Object);

            // Act
            var result = await controller.Update(reviewId, viewModel, cancellationToken);

            // Assert
            var model = Assert.IsType<ReviewViewModel>(result);
            Assert.Equal(reviewId, model.Id);
            Assert.Equal(viewModel.Title, model.Title);
        }
    }
}
