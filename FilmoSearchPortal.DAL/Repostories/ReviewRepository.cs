using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearchPortal.DAL.Repostories
{
    public class ReviewRepository : Repository<ReviewEntity>, IReviewRepository
    {
        private readonly AppDbContext _dbContext;

        public ReviewRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ReviewEntity>> GetAllAsync(int pageNumber, int pageSize, string? title, int? stars, CancellationToken cancellationToken)
        {
            IQueryable<ReviewEntity> query = _dbContext.Reviews.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                var titleInLower = title.ToLower();
                query = query.Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{titleInLower}%"));
            }

            if (stars.HasValue)
            {
                query = query.Where(x => x.Stars == stars.Value);
            }


            return await query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync(cancellationToken);
        }
    }
}
