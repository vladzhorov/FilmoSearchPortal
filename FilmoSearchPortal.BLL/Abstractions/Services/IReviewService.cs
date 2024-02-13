using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;

namespace FilmoSearchPortal.BLL.Abstractions.Services
{
    public interface IReviewService : IGenericService<ReviewEntity, Review>
    {
        Task<IEnumerable<Review>> GetAllAsync(int pageNumber, int pageSize, string? title, int? stars, CancellationToken cancellationToken);
    }
}
