using FilmoSearchPortal.DAL.Entites;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.DAL.Interfaces
{
    public interface IFilmRepository : IRepository<FilmEntity>
    {
        Task<IEnumerable<FilmEntity>> GetAllAsync(int pageNumber, int pageSize, string? title, Genre? genre, CancellationToken cancellationToken);
    }
}
