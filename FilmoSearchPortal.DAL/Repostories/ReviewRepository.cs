using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;

namespace FilmoSearchPortal.DAL.Repostories
{
    public class ReviewRepository : Repository<ReviewEntity>, IReviewRepository
    {
        public ReviewRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
