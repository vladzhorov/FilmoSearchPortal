using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.DAL.Repostories
{
    public class ActorRepository : Repository<ActorEntity>, IActorRepository
    {
        public ActorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<ActorEntity>> GetAllAsync(int pageNumber, int pageSize, string? firstName, string? lastName, ActorStatus? actorStatus, CancellationToken cancellationToken)
        {
            IQueryable<ActorEntity> query = _dbContext.Actors.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
            {
                var firstNameLower = firstName.ToLower();
                query = query.Where(x => EF.Functions.Like(x.FirstName.ToLower(), $"%{firstNameLower}%"));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                var lastNameLower = lastName.ToLower();
                query = query.Where(x => EF.Functions.Like(x.LastName.ToLower(), $"%{lastNameLower}%"));
            }

            if (actorStatus != null)
            {
                query = query.Where(x => x.ActorStatus == actorStatus);
            }

            return await query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync(cancellationToken);
        }
    }
}
