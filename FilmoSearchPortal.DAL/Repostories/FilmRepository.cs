using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;

namespace FilmoSearchPortal.DAL.Repostories
{
    public class FilmRepository : Repository<FilmEntity>, IFilmRepository
    {
        public FilmRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
