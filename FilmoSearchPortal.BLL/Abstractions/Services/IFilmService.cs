using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.BLL.Abstractions.Services
{
    public interface IFilmService : IGenericService<FilmEntity, Film>
    {
        Task<IEnumerable<Film>> GetAllAsync(int pageNumber, int pageSize, string? title, Genre? genre, CancellationToken cancellationToken);
    }
}
