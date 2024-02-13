using FilmoSearchPortal.DAL.Entites;

namespace FilmoSearchPortal.DAL.Interfaces
{
    public interface IReviewRepository : IRepository<ReviewEntity>
    {
        Task<IEnumerable<ReviewEntity>> GetAllAsync(int pageNumber, int pageSize, string? title, int? stars, CancellationToken cancellationToken);
    }
}
