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
            var filmModels = _mapper.Map<IEnumerable<Film>>(filmsEntities);
            return filmModels;
        }
    }
}
