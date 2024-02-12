using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;

namespace FilmoSearchPortal.BLL.Abstractions.Services
{
    public interface IFilmService : IGenericService<FilmEntity, Film>
    {
        Task<IEnumerable<Actor>> GetActorsForFilmAsync(Guid filmId, CancellationToken cancellationToken);
        Task<IEnumerable<Review>> GetReviewsForFilmAsync(Guid filmId, CancellationToken cancellationToken);
    }
}
