using AutoMapper;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.BLL.Services
{
    public class FilmService : GenericService<FilmEntity, Film>, IFilmService
    {
        private readonly IFilmRepository _repository;
        private readonly IMapper _mapper;

        public FilmService(IFilmRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Film>> GetAllAsync(int pageNumber, int pageSize, string? title, Genre? genre, CancellationToken cancellationToken)
        {
            var filmsEntities = await _repository.GetAllAsync(pageNumber, pageSize, title, genre, cancellationToken);
            var filmModels = filmsEntities.Select(film =>
        {
            var filmModel = _mapper.Map<Film>(film);
            filmModel.Actors = _mapper.Map<ICollection<Actor>>(film.Actors);
            filmModel.Reviews = _mapper.Map<ICollection<Review>>(film.Reviews);
            return filmModel;
        });

            return filmModels;
        }
    }
}
