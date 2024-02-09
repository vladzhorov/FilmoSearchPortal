using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;

namespace FilmoSearchPortal.DAL.Repostories
{
    public class ActorRepository : Repository<ActorEntity>, IActorRepository
    {
        public ActorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
