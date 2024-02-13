using FilmoSearchPortal.DAL.Entites;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.DAL.Interfaces
{
    public interface IActorRepository : IRepository<ActorEntity>
    {
        Task<IEnumerable<ActorEntity>> GetAllAsync(int pageNumber, int pageSize, string? firstName, string? lastName, ActorStatus? actorStatus, CancellationToken cancellationToken);
    }
}
