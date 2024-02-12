using AutoMapper;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearchPortal.BLL.Services
{
    public class FilmService : GenericService<FilmEntity, Film>, IFilmService
    {
        private readonly AppDbContext _context;

        public FilmService(IRepository<FilmEntity> repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetActorsForFilmAsync(Guid filmId, CancellationToken cancellationToken)
        {
            var film = await _context.Films.Include(f => f.Actors).FirstOrDefaultAsync(f => f.Id == filmId, cancellationToken);
            return _mapper.Map<IEnumerable<Actor>>(film.Actors);
        }

        public async override Task<IEnumerable<Film>> GetAllAsync(CancellationToken cancellationToken)
        {
            var filmsWithActorsAndReviews = await _context.Films
                .Include(f => f.Actors)
                .Include(f => f.Reviews)
                .ToListAsync(cancellationToken);

            var models = _mapper.Map<IEnumerable<Film>>(filmsWithActorsAndReviews);
            return models;
        }

        public async Task<IEnumerable<Review>> GetReviewsForFilmAsync(Guid filmId, CancellationToken cancellationToken)
        {
            var film = await _context.Films
                .Include(f => f.Reviews)
                .FirstOrDefaultAsync(f => f.Id == filmId, cancellationToken);

            if (film == null || film.Reviews == null)
                return Enumerable.Empty<Review>();

            return _mapper.Map<IEnumerable<Review>>(film.Reviews);
        }
    }
}
