using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;

namespace FilmoSearchPortal.UnitTest.TestData
{
    public static class ReviewTestData
    {
        public static (List<ReviewEntity>, List<Review>) TestGetAllReviews()
        {
            var reviewEntity1 = new ReviewEntity { Id = Guid.NewGuid(), Title = "Review 1", Stars = 4 };
            var reviewEntity2 = new ReviewEntity { Id = Guid.NewGuid(), Title = "Review 2", Stars = 5 };

            var reviewModel1 = new Review { Id = reviewEntity1.Id, Title = reviewEntity1.Title, Stars = reviewEntity1.Stars };
            var reviewModel2 = new Review { Id = reviewEntity2.Id, Title = reviewEntity2.Title, Stars = reviewEntity2.Stars };

            var reviewsEntities = new List<ReviewEntity> { reviewEntity1, reviewEntity2 };
            var reviewModels = new List<Review> { reviewModel1, reviewModel2 };

            return (reviewsEntities, reviewModels);
        }

        public static (Guid, ReviewEntity, Review) CreateReview()
        {
            var reviewId = Guid.NewGuid();
            var reviewEntity = new ReviewEntity { Id = reviewId, Title = "Review Title", Stars = 4 };
            var reviewModel = new Review { Id = reviewEntity.Id, Title = reviewEntity.Title, Stars = reviewEntity.Stars };
            return (reviewId, reviewEntity, reviewModel);
        }
    }
}
