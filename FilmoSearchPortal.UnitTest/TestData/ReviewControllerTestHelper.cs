using FilmoSearchPortal.API.ViewModels.Review;
using FilmoSearchPortal.BLL.Models;

namespace FilmoSearchPortal.UnitTest.TestData
{
    public static class ReviewControllerTestHelper
    {
        public static (List<Review> reviews, List<ReviewViewModel> reviewViewModels) TestGetAll()
        {
            var reviewId1 = Guid.NewGuid();
            var reviewId2 = Guid.NewGuid();

            var reviews = new List<Review>
            {
                new Review { Id = reviewId1,  Title = "Review 1" },
                new Review { Id = reviewId2, Title = "Review 2" }
            };

            var reviewViewModels = new List<ReviewViewModel>
            {
                new ReviewViewModel { Id = reviewId1, Title = "Review 1" },
                new ReviewViewModel { Id = reviewId2, Title = "Review 2" }
            };

            return (reviews, reviewViewModels);
        }

        public static (Guid reviewId, Review review, ReviewViewModel reviewViewModel) TestGetById()
        {
            var reviewId = Guid.NewGuid();
            var review = new Review { Id = reviewId, Title = "Test Review" };
            var reviewViewModel = new ReviewViewModel { Id = reviewId, Title = "Test Review" };
            return (reviewId, review, reviewViewModel);
        }

        public static (CreateReviewViewModel viewModel, Review review) TestCreate()
        {
            var reviewId = Guid.NewGuid();
            var viewModel = new CreateReviewViewModel { Title = "Test Review" };
            var review = new Review { Id = reviewId, Title = viewModel.Title };
            return (viewModel, review);
        }

        public static (Guid reviewId, UpdateReviewViewModel viewModel, Review review) TestUpdate()
        {
            var reviewId = Guid.NewGuid();
            var viewModel = new UpdateReviewViewModel { Title = "Updated Review" };
            var review = new Review { Id = reviewId, Title = viewModel.Title };
            return (reviewId, viewModel, review);
        }
    }
}
