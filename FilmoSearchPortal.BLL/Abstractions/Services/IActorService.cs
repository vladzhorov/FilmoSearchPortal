using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.BLL.Abstractions.Services
{
    public interface IActorService : IGenericService<ActorEntity, Actor>
    {
        Task<IEnumerable<Actor>> GetAllAsync(int pageNumber, int pageSize, string? firstName, string? lastName, ActorStatus? actorStatus, CancellationToken cancellationToken);
    }
}
