using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.DAL.Repostories
{
    public class FilmRepository : Repository<FilmEntity>, IFilmRepository
    {
        private readonly AppDbContext _dbContext;

        public FilmRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<FilmEntity>> GetAllAsync(int pageNumber, int pageSize, string? title, Genre? genre, CancellationToken cancellationToken)
        {
            IQueryable<FilmEntity> query = _dbContext.Films.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                var titleInLower = title.ToLower();
                query = query.Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{titleInLower}%"));
            }

            if (genre != null)
            {
                query = query.Where(x => x.Genre == genre);
            }



            return await query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .Include(f => f.Actors)
                             .Include(f => f.Reviews)
                             .ToListAsync(cancellationToken);
        }
    }
}
